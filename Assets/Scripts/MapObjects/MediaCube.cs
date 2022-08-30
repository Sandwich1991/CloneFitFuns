using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.Video;

public class MediaCube : Clickable
{
    [SerializeField] private MediaPlayer _cubePlayer;

    [SerializeField] private MediaReference _mediaReference;

    private GameObject _videoUI;
    private MediaPlayer _uiPlayer;
    protected override void init()
    {
        
    }

    public override void OnMouseDown()
    {
        Managers.Sound.Mute();
        
        _videoUI = Managers.Video.MediaPlayerUI;
        _uiPlayer = Managers.Video.MediaPlayer;
        
       if (_uiPlayer.OpenMedia(_mediaReference, true))
           _uiPlayer.Play();
    }
}
