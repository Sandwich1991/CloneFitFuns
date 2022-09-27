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

    private void Start()
    {
        _exitButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        _uploadButton.onClick.AddListener(() => Managers.Resource.Instantiate("UploadPostUI", gameObject.transform));
        
        GetPosts();

        Managers.Web.RefreshAction += Refresh;
    }
    
    void GetPosts()
    {
        Managers.Web.GetPosts((postList) =>
        {
            if (postList != null)
            {
                foreach (var postData in postList.getPostList)
                {
                    PostCard postCard = Managers.Resource.Instantiate("Post", _posts.transform).GetComponent<PostCard>();

                    postCard.Data = postData;

                    postCard.button.onClick.AddListener(() => GetDetailPost(postData.postId));
                }
            }
            else
            {
                LoadFaliedWindow();
            }
        });
    }
    
    void GetDetailPost(string id)
    {
        Managers.Web.GetDetailPost(id, (postData) =>
        {
            if (postData != null)
            {
                PostUI postUI = Managers.Resource.Instantiate("PostUI", gameObject.transform).GetComponent<PostUI>();

                postUI.PostData = postData.getDetailPost;
            }
            else
            {
                LoadFaliedWindow();
            }
        });
    }

    void LoadFaliedWindow()
    {
        Managers.UI.ConfirmWindow("불러오기에 실패했습니다!", transform);
    }
    
    // 포스트 수정이나 삭제가 이루어지면 포스트 목록을 새로고침
    void Refresh()
    {
        PostCard[] posts = _posts.GetComponentsInChildren<PostCard>();

        foreach (PostCard postCard in posts)
        {
            Managers.Resource.Destroy(postCard.gameObject);
        }
        
        GetPosts();
    }
}
