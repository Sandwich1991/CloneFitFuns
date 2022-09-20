using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadPost
{
    
}

public class UploadUI : MonoBehaviour
{
    [SerializeField] private InputField _titleInput;
    [SerializeField] private Text _titleText;
    [SerializeField] private InputField _contentInput;
    [SerializeField] private Text _contentText;

    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    IEnumerator Upload()
    {
        // todo content와 title이 넘어가지 않음...
        
        string url = "http://52.78.82.4/posts/post";
        
        string title = _titleText.text;
        string content = _contentText.text;
        
        WWWForm formdata = new WWWForm();
        formdata.AddField("title", title);
        formdata.AddField("content", content);

        // List<IMultipartFormSection> formdata = new List<IMultipartFormSection>();
        // formdata.Add(new MultipartFormDataSection("title", title));
        // formdata.Add(new MultipartFormDataSection("content", content));

        UnityWebRequest www = UnityWebRequest.Post(url, formdata);

        yield return www.SendWebRequest();

        if (www.error == null)
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", gameObject.transform)
                .GetComponent<ConfirmWindow>();

            window.Text = "게시가 완료되었습니다!";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
            
            // todo 화면 새로고침 비동기로 작업해야할듯...
            Managers.Web.PostChanged();
        }

        else
        {
            ConfirmWindow window = Managers.Resource.Instantiate("ConfirmWindow", gameObject.transform)
                .GetComponent<ConfirmWindow>();

            window.Text = "게시를 실패했습니다.";
            window.ConfirmButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
            Debug.Log(www.error);
        }
    }

    private void Start()
    {
        _confirmButton.onClick.AddListener(() => StartCoroutine(Upload()));
        _cancelButton.onClick.AddListener(() => Managers.Resource.Destroy(gameObject));
    }
}
