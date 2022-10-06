using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Notice
{
    public string Title;
    public string Content;
    public string Date;

    public Notice(string title, string content, string date)
    {
        Title = title;
        Content = content;
        Date = date;
    }
}

public class NoticeManager
{
    public Action NewNotice;
    public List<Notice> NoticeList = new List<Notice>();

    public void AddNotice(Notice notice)
    {
        NoticeList.Add(notice);
        NewNotice.Invoke();
    }

    public void init()
    {
        Notice hello = new Notice("어서오세요!", "환영합니다!", "22-10-5");
        Notice make = new Notice("캐릭터 만들기!", "이렇게 저렇게!", "22-10-5");
        NoticeList.Add(hello);
        NoticeList.Add(make);
    }
}
