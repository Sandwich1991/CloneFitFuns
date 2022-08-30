using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VideoController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerMoveHandler
{
    public enum PlayState
    {
        Playing,
        Pause,
        Stop
    }

    private PlayState _isplaying = PlayState.Playing;
    public PlayState IsPlaying
    {
        get { return _isplaying; }
        set
        {
            _isplaying = value;
            switch (IsPlaying)
            {
                case PlayState.Playing:
                    VideoPlay();
                    break;
                case PlayState.Pause:
                    VideoPause();
                    break;
            }
        }
    }

    private GameObject _videoPlayerUI;
    [SerializeField] private MediaPlayer _mediaPlayer;
    [SerializeField] private GameObject _controls;
    [SerializeField] private Slider _timeLine;
    [SerializeField] private GameObject _menuBar;
    [SerializeField] private GameObject _stateIcon;
    [SerializeField] private Sprite _playIcon;
    [SerializeField] private Sprite _pauseIcon;
    [SerializeField] private GameObject _options;
    [SerializeField] private float _seekTime = 5f;
    [SerializeField]private Text _playSpeedButtonText;

    private bool _isStateIconHide = false;
    private bool _isControlHide = false;

    private Image _stateImage;

    private RectTransform _displayRect;
    private bool _isUISizeMax = false;

    private float _playSpeed = 1.0f;

    // 디스플레이 클릭시 재생 / 일시정지 토글
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            StopHideCoroutines();
            
            switch (IsPlaying)
            {
                case PlayState.Playing:
                    IsPlaying = PlayState.Pause;
                    break;
            
                case PlayState.Pause:
                    IsPlaying = PlayState.Playing;
                    break;
            }
            
            StartHideCoroutines();
        }
    }
    
    // 디스플레이 위에 마우스 포인터를 올리면 컨트롤 UI 활성화
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("HideControls");
    }
    
    // 전체화면일 때 마우스 포인터를 움직이면 컨틀롤 UI 활성화
    public void OnPointerMove(PointerEventData eventData)
    {
        if (_isUISizeMax)
            StartCoroutine("HideControls");
    }
    
    public void OnClickExit()
    {
        Managers.Resource.Destroy(_videoPlayerUI);
    }

    public void OnClickWindowSize()
    {
        RectTransform canvasRect = _videoPlayerUI.GetComponent<RectTransform>();
        float menuBarHeight = _menuBar.GetComponent<RectTransform>().rect.height;
        
        switch (_isUISizeMax)
        {
            case false:
                _displayRect.sizeDelta = new Vector2(canvasRect.rect.width, canvasRect.rect.height - menuBarHeight * 2);
                _isUISizeMax = true;
                break;
            
            case true:
                _displayRect.sizeDelta = new Vector2(800, 450);
                _isUISizeMax = false;
                break;
        }
    }

    public void VideoPlay()
    {
        StopHideCoroutines();
        _mediaPlayer.Play();
        _stateImage.sprite = _playIcon;
        StartHideCoroutines();
    }

    public void VideoPause()
    {
        StopHideCoroutines();
        _mediaPlayer.Pause();
        _stateImage.sprite = _pauseIcon;
        StartHideCoroutines();
    }

    public void VideoRewind()
    {
        StopHideCoroutines();
        _mediaPlayer.Control.Seek(_mediaPlayer.Control.GetCurrentTime() - _seekTime);
        StartHideCoroutines();
    }
    
    public void VideoForward()
    {
        StopHideCoroutines();
        _mediaPlayer.Control.Seek(_mediaPlayer.Control.GetCurrentTime() + _seekTime);
        StartHideCoroutines();
    }

    public void OptionsButton()
    {
        StopHideCoroutines();
        switch (_options.activeSelf)
        {
            case false:
                _options.SetActive(true);
                break;
            
            case true:
                _options.SetActive(false);
                break;
        }
        StartHideCoroutines();
    }
    
    public void ChangePlaySpeed()
    {
        StopHideCoroutines();
        
        switch (_playSpeed)
        {
            case 1.0f:
                _playSpeed = 1.2f;
                _mediaPlayer.PlaybackRate = _playSpeed;
                _playSpeedButtonText.text = $"재생속도 : x {_playSpeed}";
                break;
            
            case 1.2f:
                _playSpeed = 1.5f;
                _mediaPlayer.PlaybackRate = _playSpeed;
                _playSpeedButtonText.text = $"재생속도 : x {_playSpeed}";
                break;
            
            case 1.5f:
                _playSpeed = 2.0f;
                _mediaPlayer.PlaybackRate = _playSpeed;
                _playSpeedButtonText.text = $"재생속도 : x {_playSpeed}";
                break;
            
            case 2.0f:
                _playSpeed = 1.0f;
                _mediaPlayer.PlaybackRate = _playSpeed;
                _playSpeedButtonText.text = $"재생속도 : x {_playSpeed}";
                break;
        }
        
        StartHideCoroutines();
    }

    IEnumerator HideStateIcon()
    {
        float delaySeconds = 2f;
        
        switch (_isStateIconHide)
        {
            case false:
                yield return new WaitForSeconds(delaySeconds);
                _stateIcon.SetActive(false);
                _isStateIconHide = true;
                break;
            
            case true:
                _stateIcon.SetActive(true);
                _isStateIconHide = false;
                StartCoroutine("HideStateIcon");
                break;
        }
    }

    IEnumerator HideControls()
    {
        float delaySeconds = 5f;
        
        switch (_isControlHide)
        {
            case false:
                yield return new WaitForSeconds(delaySeconds);
                _controls.SetActive(false);
                _options.SetActive(false);
                _isControlHide = true;
                break;
            
            case true:
                _controls.SetActive(true);
                _isControlHide = false;
                StartCoroutine("HideControls");
                break;
        }
    }

    void StartHideCoroutines()
    {
        StartCoroutine("HideStateIcon");
        StartCoroutine("HideControls");
    }

    void StopHideCoroutines()
    {
        StopCoroutine("HideStateIcon");
        StopCoroutine("HideControls");
    }

    void GetTimeLine()
    {
        _timeLine.minValue = 0;
        _timeLine.maxValue = (float)_mediaPlayer.Info.GetDuration();
    }

    void GetTimeLineHandle()
    {
        _timeLine.value = (float)_mediaPlayer.Control.GetCurrentTime();
    }

    // 타임라인 슬라이더 컴포넌트의 이벤트
    public void SetTimeLine()
    {
        _mediaPlayer.Control.Seek(_timeLine.value);
    }
    
    private void Start()
    {
        _videoPlayerUI = transform.parent.gameObject;
        _displayRect = GetComponent<RectTransform>();
        _stateImage = _stateIcon.GetComponent<Image>();
        StartCoroutine("HideStateIcon");
        StartCoroutine("HideControls");

    }
    
    private void Update()
    {
        GetTimeLine();
        GetTimeLineHandle();
    }
}
