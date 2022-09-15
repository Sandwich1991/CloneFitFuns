using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginSeneUI : MonoBehaviour
{
    [SerializeField] private InputField _nicknameInput;
    [SerializeField] private GameObject _warningWindow;
    [SerializeField] private Button _warningComfirmButton;
    [SerializeField] private GameObject _confirmWindow;
    [SerializeField] private Text confirmWindowText;
    [SerializeField] private Button _nicknameConfirmButton;
    [SerializeField] private Button _nicknameCancelButton;

    bool IsInputEmpty()
    {
        if (_nicknameInput.text == String.Empty)
        {
            _warningWindow.SetActive(true);
            _warningComfirmButton.onClick.AddListener(() => _warningWindow.SetActive(false));
            return true;
        }
        else
        {
            return false;
        }
    }

    void StartGame()
    {
        _confirmWindow.SetActive(true);
        confirmWindowText.text = $"{_nicknameInput.text}(으)로 캐릭터를 생성합니다!";

        _nicknameConfirmButton.onClick.AddListener(() =>
        {
            _confirmWindow.SetActive(false);
            Managers.Player.Nickname = _nicknameInput.text;
            Managers.Scene.LoadScene(Define.Scene.Game);
        });
        
        _nicknameCancelButton.onClick.AddListener(() => _confirmWindow.SetActive(false));
    }

    public void ComfirmButton()
    {
        if (IsInputEmpty())
            return;
        
        StartGame();
    }
}
