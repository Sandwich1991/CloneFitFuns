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

    private void Start()
    {
        _confirmButton.onClick.AddListener(() => UploadPost());

        _cancelButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }

    // 인풋필드의 텍스트를 이용해 포스트 생성
    void UploadPost()
    {
        Managers.Web.CreatePost(_title.text, _content.text, (succeed) =>
        {
            if (succeed)
            {
                Managers.UI.ConfirmWindow("게시에 성공했습니다!", transform, true);
            }
            else
            {
                Managers.UI.ConfirmWindow("게시에 실패했습니다!", transform);
            }
        } );
    }
}
