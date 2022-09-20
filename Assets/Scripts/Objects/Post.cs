using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    // private PostData _postData;
    // public PostData PostData
    // {
    //     get { return _postData; }
    //     set { _postData = value; }
    // }
    
    [SerializeField] private Text _title;
    public string Title
    {
        get { return _title.text;}
        set { _title.text = value; }
    }

    [SerializeField] private Text _content;
    public string Content
    {
        get { return _content.text;}
        set { _content.text = value; }
    }
    
    [SerializeField] private Text _date;
    public string Date
    {
        get { return _date.text;}
        set { _date.text = value; }
    }
    
    public Button Button;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }
    
    // private void Start()
    // {
    //     _title.text = _postData.title;
    //     _content.text = _postData.content;
    //     _date.text = _postData.createdAt.Substring(0, 10);
    // }
    
    // todo 주석처리 부분에서 null 참조 에러 발생
}
