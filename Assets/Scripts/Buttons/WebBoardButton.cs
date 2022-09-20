using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBoardButton : MonoBehaviour
{
    private string _webBoardUIPath = "WebBoardUI";
    private GameObject _webBoard;
    [SerializeField] private GameObject _mainUI;
    

    public void LoadWebBorad()
    {
        _webBoard = Managers.Resource.Instantiate(_webBoardUIPath, _mainUI.transform);
    }

    public void DestroyBoard()
    {
        Managers.Resource.Destroy(_webBoard);
    }
}
