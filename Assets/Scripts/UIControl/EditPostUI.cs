using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPostUI : MonoBehaviour
{
    public InputField _titleInput;
    public Text _title;
    public InputField _contentInput;
    public Text _content;

    public string _postId;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private string _url = "http://52.78.82.4/posts/";

    private void Start()
    {
        _confirmButton.onClick.AddListener(EditPost);
        _cancelButton.onClick.AddListener(() => EditCancel());
        _exitButton.onClick.AddListener(() => EditCancel());
    }

    void EditPost()
    {
        StartCoroutine(Managers.Web.EditPost(_url + _postId, _title.text, _content.text, gameObject.transform));
    }

    void EditCancel()
    {
        WarningWindow window = Managers.Resource.Instantiate("WarningWindow", transform)
            .GetComponent<WarningWindow>();

        window.Text = "수정을 취소하시겠습니까?";
        window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }
}
