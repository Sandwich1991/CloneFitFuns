using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeButton : MonoBehaviour
{
    [SerializeField] private GameObject redDot;
    
    private NoticeBoardUI _noticeBoard;
    private Button _button;
    private bool _isActive = false;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ToggleNoticeBoard);

        Managers.Notice.NewNotice += EnableRedDot;
    }

    void ToggleNoticeBoard()
    {
        if (!_isActive)
        {
            _noticeBoard = Managers.Resource.Instantiate("NoticeBoardUI", Game.MainUI)
                .GetComponent<NoticeBoardUI>();
            
            _noticeBoard.NoticeBoardOpen += DisableRedDot;
        }
        else
        {
            Managers.Resource.Destroy(_noticeBoard.gameObject);
        }

        _isActive = !_isActive;
    }

    void EnableRedDot()
    {
        redDot.SetActive(true);
    }
    
    void DisableRedDot()
    {
        redDot.SetActive(false);
    }
}
