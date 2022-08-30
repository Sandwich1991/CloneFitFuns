using System;
using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

public class PlaybackRate : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;
    private Text _playRatioText;
    private float _playRatio;

    private void Start()
    {
        _playRatio = 1.0f;
        
        _playRatioText = transform.GetChild(0).GetComponent<Text>();
        _playRatioText.text = $"재생속도 : x {_playRatio}";
    }

    public void ChangePlaySpeed()
    {
        switch (_playRatio)
        {
            case 1.0f:
                _playRatio = 1.2f;
                _mediaPlayer.PlaybackRate = _playRatio;
                _playRatioText.text = $"재생속도 : x {_playRatio}";
                break;
            
            case 1.2f:
                _playRatio = 1.5f;
                _mediaPlayer.PlaybackRate = _playRatio;
                _playRatioText.text = $"재생속도 : x {_playRatio}";
                break;
            
            case 1.5f:
                _playRatio = 2.0f;
                _mediaPlayer.PlaybackRate = _playRatio;
                _playRatioText.text = $"재생속도 : x {_playRatio}";
                break;
            
            case 2.0f:
                _playRatio = 1.0f;
                _mediaPlayer.PlaybackRate = _playRatio;
                _playRatioText.text = $"재생속도 : x {_playRatio}";
                break;
        }
    }
    
}
