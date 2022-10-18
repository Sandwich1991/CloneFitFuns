using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailNoticeUI : MonoBehaviour
{
    [SerializeField] private Text titleComp;
    [SerializeField] private Text dateComp;
    [SerializeField] private Text contentComp;
    [SerializeField] private Button closeButton;

    public string title;
    public string date;
    public string content;

    private void Start()
    {
        titleComp.text = title;
        dateComp.text = date;
        contentComp.text = content;
        
        closeButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }
}
