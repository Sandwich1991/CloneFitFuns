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
    
    [SerializeField] private Text _title;
    [SerializeField] private Text _date;
    [SerializeField] private Text _content;

    private string _url = "http://52.78.82.4/posts";

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
        
        _title.text = PostData.title;
        _date.text = PostData.createdAt.Substring(0, 10);
        _content.text = PostData.content;
    }

    void DeleteButton()
    {
        WarningWindow window = Managers.Resource.Instantiate("WarningWindow", gameObject.transform)
            .GetComponent<WarningWindow>();
        window.Text = "정말로 삭제하시겠습니까?";
            
        window.ConfirmButton.onClick.AddListener(() =>
        {
            Managers.Resource.Destroy(window.gameObject);
            StartCoroutine(DeletePost());
        });
    }

    IEnumerator DeletePost()
    {
        UnityWebRequest www = UnityWebRequest.Delete(_url + "/" + PostData.postId);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", gameObject.transform)
                .GetComponent<ConfirmWindow>();

            window.Text = "삭제되었습니다!";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
            
            Managers.Web.PostChanged();
        }
        else
            Debug.Log(www.error);
    }
    
    
}
