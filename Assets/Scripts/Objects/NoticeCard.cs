using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeCard : MonoBehaviour
{
    [SerializeField] private Text titleComp;
    [SerializeField] private Text dateComp;

    public string title;
    public string content;
    public string date;

    private Button _button;

    private void Start()
    {
        titleComp.text = title;
        dateComp.text = date;

        SetButton();
    }

    void SetButton()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadDetailNotice);
    }

    void LoadDetailNotice()
    {
        DetailNoticeUI detailNotice = Managers.Resource
            .Instantiate("DetailNoticeUI", Game.MainUI).GetComponent<DetailNoticeUI>();

        detailNotice.title = title;
        detailNotice.date = date;
        detailNotice.content = content;
    }
}
