using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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

[Serializable]
public class UploadPost
{
    public string title;
    public string content;

    public UploadPost(string title, string content)
    {
        this.title = title;
        this.content = content;
    }
}

public class WebManager
{
    public Action RefreshAction;

    public void RefreshPosts()
    {
        RefreshAction.Invoke();
    }
    
    public void GetPosts(Action<PostList> evt)
    {
        Managers.CoroutineHelper(GetPostsCoroutine(evt.Invoke));
    }
    
    public void GetDetailPost(string id, Action<DetailPost> evt)
    {
        Managers.CoroutineHelper(GetDetailPostCoroutine(id, evt.Invoke));
    }

    public void CreatePost(string title, string content, Action<bool> haveSucceed)
    {
        Managers.CoroutineHelper(CreatePostCoroutine(title, content, haveSucceed.Invoke));
    }
    
    public void PutPost(string id, string title, string content, Action<bool> haveSucceed)
    {
        Managers.CoroutineHelper(PutPostCoroutine(id, title, content, haveSucceed.Invoke));
    }
    
    public void DeletePost(string id, Action<bool> haveSucceed)
    {
        Managers.CoroutineHelper(DeletePostCoroutine(id, haveSucceed.Invoke));
    }
    
    
    private IEnumerator GetPostsCoroutine(Action<PostList> evt)
    {

        UnityWebRequest www = UnityWebRequest.Get(Define.URL);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            PostList postList = JsonUtility.FromJson<PostList>(www.downloadHandler.text);

            evt.Invoke(postList);
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    
    private IEnumerator GetDetailPostCoroutine(string id, Action<DetailPost> evt)
    {
        UnityWebRequest www = UnityWebRequest.Get(Define.URL + "/" + id);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            DetailPost post = JsonUtility.FromJson<DetailPost>(www.downloadHandler.text);

            evt.Invoke(post);
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    
    private IEnumerator CreatePostCoroutine(string title, string content, Action<bool> isSucceed)
    {
        UnityWebRequest www = PostOrPutRequest("POST", title, content);
        
        yield return www.SendWebRequest();

        PostProcessWebRequest(www, isSucceed.Invoke);
        
        www.Dispose();
    }
    
    private IEnumerator PutPostCoroutine(string id, string title, string content, Action<bool> isSucceed)
    {
        UnityWebRequest www = PostOrPutRequest("PUT", title, content, id);
        
        yield return www.SendWebRequest();
        
        PostProcessWebRequest(www, isSucceed.Invoke);
        
        www.Dispose();
    }
    
    private IEnumerator DeletePostCoroutine(string id, Action<bool> isSucceed)
    {
        UnityWebRequest www = UnityWebRequest.Delete(Define.URL + "/" + id);

        yield return www.SendWebRequest();

        PostProcessWebRequest(www, isSucceed.Invoke);
        
        www.Dispose();
    }

    
    
    private void PostProcessWebRequest(UnityWebRequest result, Action<bool> isSucceed)
    {
        if (result.error == null)
        {
            isSucceed.Invoke(true);
            RefreshPosts();
        }
        else
        {
            isSucceed.Invoke(false);
            Debug.Log(result.error);
        }
    }
    
    private UnityWebRequest PostOrPutRequest(string method, string title, string content, string id = null)
    {
        string url = method == "POST" ? Define.URL + "/post" : Define.URL + "/" + id;
        
        UploadPost post = new UploadPost(title, content);
        string json = JsonUtility.ToJson(post);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(url, method);
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        return www;
    }

}
