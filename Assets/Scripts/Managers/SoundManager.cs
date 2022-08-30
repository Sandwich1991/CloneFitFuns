using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.SoundType.MaxCount];

    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    public Define.SoundState IsPlaying = Define.SoundState.Playing;


    public void init()
    {
        GameObject root = GameObject.FindWithTag("SoundManager");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            root.tag = "SoundManager";
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.SoundType));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.SoundType.Bgm].loop = true;
            _audioSources[(int)Define.SoundType.Bgm].volume = 0.5f;
            _audioSources[(int)Define.SoundType.Effect].volume = 1.0f;
        }
    }

    void Clear()
    {
        foreach (AudioSource source in _audioSources)
        {
            source.clip = null;
            source.Stop();
        }
        
        _audioClips.Clear();
    }

    AudioClip GetOrAddAudioClip(string path, Define.SoundType type = Define.SoundType.Effect)
    {
        if (path.Contains("Sounds") == false)
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null;

        if (type == Define.SoundType.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing! {path}");
        }

        return audioClip;
    }

    public void Play(AudioClip audioClip, Define.SoundType type = Define.SoundType.Effect, float pitch = 1f)
    {
        if (audioClip == null)
        {
            return;
        }

        if (type == Define.SoundType.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.SoundType.Bgm];

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        else
        {
            AudioSource audioSource = _audioSources[(int)Define.SoundType.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Define.SoundType type, float pitch)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Mute()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.mute = true;
        }

        IsPlaying = Define.SoundState.Mute;
    }

    public void UnMute()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.mute = false;
        }

        IsPlaying = Define.SoundState.Playing;
    }

    public void OnUpdate()
    {
        switch (IsPlaying)
        {
            case Define.SoundState.Playing:
                UnMute();
                break;
            
            case Define.SoundState.Mute:
                Mute();
                break;
        }
    }
}
