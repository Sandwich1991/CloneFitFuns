using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostUI : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;
    
    [SerializeField] private Text _date;
    [SerializeField] private Text _title;
    [SerializeField] private Text _titlePlaceHolder;
    [SerializeField] private Text _content;
    [SerializeField] private Text _contentPlaceHolder;

    private PostData _postData;
    public PostData PostData
    {
        get { return _postData; }
        set { _postData = value; }
    }

    private void Start()
    {
        _exitButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
        _deleteButton.onClick.AddListener(DeleteButton);
        _editButton.onClick.AddListener(EditButton);
        
        _title.text = PostData.title;
        _date.text = PostData.createdAt.Substring(0, 10);
        _content.text = PostData.content;
    }

    void DeleteButton()
    {
        Managers.UI.WarningWindow("정말 삭제하시겠습니까?", transform, true, () =>
        {
            Managers.UI.ConfirmWindow("삭제가 완료되었습니다!", transform, true);
            DeletePost();
        });
    }

    void EditButton()
    {
        Managers.UI.WarningWindow("수정하시겠습니까?", transform, true, EditPost);
    }

    // 포스트 삭제
    void DeletePost()
    {
        Managers.Web.DeletePost(_postData.id, (succeed) =>
        {
            if (succeed)
            {
                Managers.UI.ConfirmWindow("삭제가 성공되었습니다!", transform, true);
                Managers.Resource.Destroy(gameObject);
            }
            else
            {
                Managers.UI.ConfirmWindow("오류가 발생했습니다!", transform);
            }
        });
    }
    
    // 포스트 수정
    void EditPost()
    {
        EditPostUI editPostUI = Managers.Resource.Instantiate("EditPostUI", gameObject.transform.parent)
            .GetComponent<EditPostUI>();
        
        Managers.Resource.Destroy(gameObject);

        // 업로드 ui의 텍스트를 원래 포스트의 내용으로 미리 채움
        editPostUI._titleInput.text = _postData.title;
        editPostUI._contentInput.text = _postData.content;
        editPostUI._postId = _postData.postId;
    }
}
