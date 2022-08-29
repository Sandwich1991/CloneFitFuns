using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Sprite _soundPlaying;
    private string _soundPlayingPath = "Icons/SoundOn";
    
    private Sprite _mute;
    private string _mutePath = "Icons/SoundMute";
    
    private Image _imageComponent;

    private void Start()
    {
        _soundPlaying = Managers.Resource.Load<Sprite>(_soundPlayingPath);
        _mute = Managers.Resource.Load<Sprite>(_mutePath);
        _imageComponent = GetComponent<Image>();
    }

    void ToggleImage()
    {
        switch (Managers.Sound.IsPlaying)
        {
            case Define.SoundState.Playing:
                _imageComponent.sprite = _soundPlaying;
                break;
            
            case Define.SoundState.Mute:
                _imageComponent.sprite = _mute;
                break;
        }
    }

    public void ToggleMute()
    {
        switch (Managers.Sound.IsPlaying)
        {
            case Define.SoundState.Playing:
                Managers.Sound.IsPlaying = Define.SoundState.Mute;
                ToggleImage();
                break;
            
            case Define.SoundState.Mute:
                Managers.Sound.IsPlaying = Define.SoundState.Playing;
                ToggleImage();
                break;
        }
    }
    
    
}
