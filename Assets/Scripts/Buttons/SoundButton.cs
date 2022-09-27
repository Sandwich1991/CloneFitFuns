using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    private Sprite _spritePlaying;
    private const string PathPlaying = "Icons/SoundOn";
    
    private Sprite _spriteMute;
    private const string PathMute = "Icons/SoundMute";
    
    private Image _image;

    private Button _button;

    private void Start()
    {
        _spritePlaying = Managers.Resource.Load<Sprite>(PathPlaying);
        _spriteMute = Managers.Resource.Load<Sprite>(PathMute);

        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(ToggleMute);
    }
    
    private void ToggleMute()
    {
        Managers.Sound.IsPlaying = !Managers.Sound.IsPlaying;
        _image.sprite = Managers.Sound.IsPlaying ? _spritePlaying : _spriteMute;
    }
}
