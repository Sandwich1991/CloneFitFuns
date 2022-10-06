using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeDebug : MonoBehaviour
{
    [SerializeField] private Text titleComp;
    [SerializeField] private Text dateComp;
    [SerializeField] private Text contentComp;
    [SerializeField] private Button submitButton;

    void NoticeSubmit()
    {
        string title = titleComp.text;
        string date = dateComp.text;
        string content = contentComp.text;

        Notice notice = new Notice(title, content, date);
        
        Managers.Notice.AddNotice(notice);
    }

    private void Start()
    {
        submitButton.onClick.AddListener(NoticeSubmit);
    }
}
