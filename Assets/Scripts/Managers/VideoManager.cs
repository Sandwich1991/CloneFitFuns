using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class VideoManager
{
    private GameObject _mediaPlayerUI;
    private MediaPlayer _mediaPlayer;
    private const string PathMediaPlayerUI = "VideoPlayerUI";

    public GameObject MediaPlayerUI
    {
        get
        {
            _mediaPlayerUI = GenerateUI();
            return _mediaPlayerUI;
        }
    }

    public MediaPlayer MediaPlayer
    {
        get
        {
            _mediaPlayer = GeneratePlayer();
            return _mediaPlayer;
        }
    }

    GameObject GenerateUI()
    {
        if (_mediaPlayerUI == null)
        {
            GameObject go = Managers.Resource.Instantiate(PathMediaPlayerUI);
            return go;
        }

        return _mediaPlayerUI;
    }

    MediaPlayer GeneratePlayer()
    {
        if (_mediaPlayer == null)
        {
            GenerateUI();
            MediaPlayer mediaPlayer = _mediaPlayerUI.transform.Find("MediaPlayer").GetComponent<MediaPlayer>();
            return mediaPlayer;
        }

        return _mediaPlayer;
    }
}
