using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostCard : MonoBehaviour
{
    public Button button;
    
    [SerializeField] private Text title;
    [SerializeField] private Text content;
    [SerializeField] private Text date;

    public PostData Data { get; set; }
    
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        title.text = Data.title;
        content.text = Data.content;
        date.text = Data.createdAt.Substring(0, 10);
    }
}
