using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Fields
    private static Managers _instance = null;
    public static Managers Instance { get { init(); return _instance; } }
    
    // Managers
    private InputManager _input = new InputManager();
    private ResourceManager _resource = new ResourceManager();
    private PlayerManager _player = new PlayerManager();
    private SoundManager _sound = new SoundManager();
    private VideoManager _video = new VideoManager();
    private SceneManagerEX _sceneManager = new SceneManagerEX();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static PlayerManager Player { get { return Instance._player; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static VideoManager Video { get { return Instance._video; } }
    public static SceneManagerEX Scene { get { return Instance._sceneManager; } }

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
                _instance._sceneManager.init();
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
    
    // MonoBehaviour
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
