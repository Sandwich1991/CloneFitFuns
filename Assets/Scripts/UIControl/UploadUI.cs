using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadUI : MonoBehaviour
{
    public InputField _titleInput;
    public Text _title;
    public InputField _contentInput;
    public Text _content;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    
    [SerializeField] string _url = "http://52.78.82.4/posts/post";
    
    private void Start()
    {
        _confirmButton.onClick.AddListener(() => UploadPost());

        _cancelButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }

    // 인풋필드의 텍스트를 이용해 포스트 생성
    void UploadPost()
    {
        StartCoroutine(Managers.Web.CreatePost(_url, _title.text, _content.text, gameObject.transform));
    }
}
