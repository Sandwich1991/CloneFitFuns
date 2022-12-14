using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MouseEvent
    {
        Click,
        Press,
    }
    
    public enum PlayerState
    {
        Idle,
        Walk,
    }
    
    public enum SoundType
    {
        Bgm,
        Effect,
        MaxCount
    }
    
    public enum SoundState
    {
        Playing,
        Mute,
    }

    public enum Layer
    {
        Block = 6,
        Clickable = 7,
    }
    
    public enum Scene
    {
        Login,
        Game,
    }

    public static string URL = "http://52.78.82.4/posts";

}
