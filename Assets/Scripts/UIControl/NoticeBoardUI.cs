using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeBoardUI : MonoBehaviour
{
    public Action NoticeBoardOpen;
    
    [SerializeField] private Transform container;

    private void Start()
    {
        MakeCards();
        
        NoticeBoardOpen.Invoke();
    }

    void MakeCards()
    {
        foreach (var notice in Managers.Notice.NoticeList)
        {
            GameObject go = Managers.Resource.Instantiate("NoticeCard", container);

            NoticeCard card = go.GetComponent<NoticeCard>();

            card.title = notice.Title;
            card.date = notice.Date;
            card.content = notice.Content;
        }
    }
}
