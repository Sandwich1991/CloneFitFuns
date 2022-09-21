using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;



public class WebBoardUI : MonoBehaviour
{
    [SerializeField] private GameObject _posts;
    
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _uploadButton;

    [SerializeField] string _url = "http://52.78.82.4/posts";

    private void Start()
    {
        _exitButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        _uploadButton.onClick.AddListener(() => Managers.Resource.Instantiate("UploadPostUI", gameObject.transform));
        
        GetPosts();

        Managers.Web.RefreshAction += Refresh;
    }
    
    // 웹 매니져의 GetPosts 코루틴 실행
    // GetPosts((url(주소), tranform(경고창 띄울 부모UI), evt(받아온 포스트목록으로 취할 액션 ));
    void GetPosts()
    {
        StartCoroutine(Managers.Web.GetPosts(_url, gameObject.transform, (postList) =>
        {
            foreach (var posting in postList.getPostList)
            {
                Post post = Managers.Resource.Instantiate("Post", _posts.transform).GetComponent<Post>();

                post.PostData = posting;

                post.Button.onClick.AddListener(() => GetPost(posting.postId) );
            }
        }));
    }
    
    // 웹 매니져의 GetPost 코루틴 실행 (포스트 상세 조회)
    // GetPost((url(주소), id(포스트id), tranform(경고창 띄울 부모UI), evt(받아온 포스트목록으로 취할 액션 ));
    void GetPost(string id)
    {
        StartCoroutine(Managers.Web.GetPost(_url, id, gameObject.transform, (postData) =>
        {
            PostUI postUI = Managers.Resource.Instantiate("PostUI", gameObject.transform).GetComponent<PostUI>();

            postUI.PostData = postData.getDetailPost;
        }));
    }
    
    // 포스트 수정이나 삭제가 이루어지면 포스트 목록을 새로고침
    void Refresh()
    {
        Post[] posts = _posts.GetComponentsInChildren<Post>();

        foreach (Post post in posts)
        {
            Managers.Resource.Destroy(post.gameObject);
        }
        
        GetPosts();
    }
}
