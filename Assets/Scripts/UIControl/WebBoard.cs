using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class Post
{
    public string Data;
    public string[] Posts;
    public string Id;
    public string Title;
    public string Content;

    public Post(string data)
    {
        
    }

}

public class WebBoard : MonoBehaviour
{
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _postsText;
    
    [SerializeField] private Button _exitButton;
    
    string _homeUrl = "http://52.78.82.4/";
    string _postsUrl = "http://52.78.82.4/posts";

    private void Start()
    {
        _exitButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        StartCoroutine(GetHome());
        StartCoroutine(GetPosts());
    }

    IEnumerator GetHome()
    {
        UnityWebRequest www = UnityWebRequest.Get(_homeUrl);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            _mainText.text = www.downloadHandler.text;
        }
        else
        {
            _mainText.text = www.error;
        }
    }

    IEnumerator GetPosts()
    {
        UnityWebRequest www = UnityWebRequest.Get(_postsUrl);
        
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            string loadedText = www.downloadHandler.text;

            Post posts = new Post(loadedText);

            _postsText.text = posts.Posts[0];
        }
        else
        {
            _postsText.text = www.error;
        }
    }
}
