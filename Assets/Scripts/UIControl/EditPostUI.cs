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

    private void Start()
    {
        _confirmButton.onClick.AddListener(EditPost);
        _cancelButton.onClick.AddListener(() => EditCancel());
        _exitButton.onClick.AddListener(() => EditCancel());
    }

    void EditPost()
    {
        Managers.Web.PutPost(_postId, _title.text, _content.text, (succeed) =>
        {
            if (succeed)
            {
                Managers.UI.ConfirmWindow("수정이 완료되었습니다!", transform, true);
            }
            else
            {
                Managers.UI.ConfirmWindow("오류가 발생했습니다!", transform);
            }
        });
    }

    void EditCancel()
    {
        WarningWindow window = Managers.Resource.Instantiate("WarningWindow", transform)
            .GetComponent<WarningWindow>();

        window.Text = "수정을 취소하시겠습니까?";
        window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        Managers.Resource.Destroy(gameObject);
    }
}
