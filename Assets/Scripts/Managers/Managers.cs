using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance = null;
    private InputManager _input = new InputManager();
    private ResourceManager _resource = new ResourceManager();
    private PlayerManager _player = new PlayerManager();
    private SoundManager _sound = new SoundManager();
    private VideoManager _video = new VideoManager();
    private SceneManagerEX _sceneManager = new SceneManagerEX();
    private WebManager _web = new WebManager();
    private UIManager _ui = new UIManager();
    private NoticeManager _notice = new NoticeManager();
    
    public static Managers Instance { get { init(); return _instance; } }
    public static InputManager Input => Instance._input;
    public static ResourceManager Resource => Instance._resource;
    public static PlayerManager Player => Instance._player;
    public static SoundManager Sound => Instance._sound;
    public static VideoManager Video => Instance._video;
    public static SceneManagerEX Scene => Instance._sceneManager;
    public static WebManager Web => Instance._web;
    public static UIManager UI => Instance._ui;
    public static NoticeManager Notice => Instance._notice;

    // Methods
    static void init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");

            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<Managers>();
                
                _instance._sound.init();
                _instance._notice.init();
            }
        }
    }

    public static void CoroutineHelper(IEnumerator coroutine)
    {
        _instance.StartCoroutine(coroutine);
    }

    public static void Clear()
    {
        Input.Clear();
    }
    
    
    private void Start()
    {
        init();
    }

    private void Update()
    {
        _input.OnUpdate();
        _sound.OnUpdate();
    }
}
