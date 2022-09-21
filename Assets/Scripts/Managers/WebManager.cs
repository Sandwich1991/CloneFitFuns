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

    public void PostChanged()
    {
        RefreshAction.Invoke();
    }

    // 포스트 목록 조회
    public IEnumerator GetPosts(string url, Transform uiParent, Action<PostList> evt)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            PostList postList =  JsonUtility.FromJson<PostList>(www.downloadHandler.text);

            evt.Invoke(postList);
        }
        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();

            window.Text = "로드에 실패했습니다";
            Debug.Log(www.error);
            window.ConfirmButton.onClick.AddListener( () => Managers.Resource.Destroy(window.gameObject));
        }
    }
    
    // 포스트 상세 조회
    public IEnumerator GetPost(string url, string id, Transform uiParent, Action<DetailPost> evt)
    {
        UnityWebRequest www = UnityWebRequest.Get(url + "/" + id);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            DetailPost post = JsonUtility.FromJson<DetailPost>(www.downloadHandler.text);

            evt.Invoke(post);
        }
        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();

            window.Text = "로드에 실패했습니다";
            Debug.Log(www.error);
            window.ConfirmButton.onClick.AddListener( () => Managers.Resource.Destroy(window.gameObject));
        }
    }
    
    // 포스트 생성
    public IEnumerator CreatePost(string url, string title, string content, Transform uiParent)
    {
        UploadPost post = new UploadPost(title, content);
        string json = JsonUtility.ToJson(post);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        
        yield return www.SendWebRequest();
        
        if (www.error == null)
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();
        
            window.Text = "게시가 완료되었습니다!";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(uiParent.gameObject));
            
            Managers.Web.PostChanged();
        }
        
        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();
        
            window.Text = "게시를 실패했습니다.";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(window.gameObject));
            Debug.Log(www.error);
        }
        
        www.Dispose();
    }
    
    // 포스트 수정
    public IEnumerator EditPost(string url, string title, string content, Transform uiParent)
    {
        UploadPost post = new UploadPost(title, content);
        string json = JsonUtility.ToJson(post);
        byte[] body = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest(url, "PUT");
        www.uploadHandler = new UploadHandlerRaw(body);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        
        yield return www.SendWebRequest();
        
        if (www.error == null)
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();
        
            window.Text = "수정이 완료되었습니다!";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(uiParent.gameObject));
            
            Managers.Web.PostChanged();
        }
        
        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();
        
            window.Text = "수정에 실패했습니다.";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(window.gameObject));
            Debug.Log(www.error);
        }
        
        www.Dispose();
    }

    // 포스트 삭제
    public IEnumerator DeletePost(string url, string id, Transform uiParent)
    {
        UnityWebRequest www = UnityWebRequest.Delete(url + "/" + id);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();

            window.Text = "삭제되었습니다!";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(uiParent.gameObject));
            
            Managers.Web.PostChanged();
        }
        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", uiParent)
                .GetComponent<ConfirmWindow>();

            window.Text = "삭제에 실패했습니다";
            Debug.Log(www.error);
            window.ConfirmButton.onClick.AddListener( () => Managers.Resource.Destroy(window.gameObject));
        }
    }

}
