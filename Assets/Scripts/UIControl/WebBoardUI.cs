using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class PostData
{
    public string _id;
    public string title;
    public string content;
    public string createdAt;
    public string updateAt;
    public string postId;
    public string id;
}

[Serializable]
public class PostList
{
    public PostData[] getPostList;
}

[Serializable]
public class DetailPost
{
    public PostData getDetailPost;
}

public class WebBoardUI : MonoBehaviour
{
    [SerializeField] private GameObject _posts;
    
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _uploadButton;

    string _url = "http://52.78.82.4/posts";

    private void Start()
    {
        _exitButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        _uploadButton.onClick.AddListener(() => Managers.Resource.Instantiate("UploadPostUI", gameObject.transform));
        StartCoroutine(GetPosts());

        Managers.Web.RefreshAction += Refresh;
    }
    
    IEnumerator GetPosts()
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            string loadedText = www.downloadHandler.text;

            PostList postList =  JsonUtility.FromJson<PostList>(loadedText);

            foreach (var posting in postList.getPostList)
            {
                Post post = Managers.Resource.Instantiate("Post", _posts.transform).GetComponent<Post>();

                // post.PostData = posting;

                post.Title = posting.title;

                post.Content = posting.content;

                post.Date = posting.createdAt.Substring(0, 10);

                post.Button.onClick.AddListener(()=>
                {
                    StartCoroutine(GetPost(posting._id));
                });
            }
        }
        else
            Debug.Log("Error!");
    }

    IEnumerator GetPost(string id)
    {
        UnityWebRequest www = UnityWebRequest.Get(_url + "/" + id);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            DetailPost post = JsonUtility.FromJson<DetailPost>(www.downloadHandler.text);

            PostUI postUI = Managers.Resource.Instantiate("PostUi", gameObject.transform).GetComponent<PostUI>();

            postUI.PostData = post.getDetailPost;
        }
        else
            Debug.Log(www.error);
    }

    void Refresh()
    {
        Post[] posts = _posts.GetComponentsInChildren<Post>();

        foreach (Post post in posts)
        {
            Managers.Resource.Destroy(post.gameObject);
        }
        
        StartCoroutine(GetPosts());
    }
}
