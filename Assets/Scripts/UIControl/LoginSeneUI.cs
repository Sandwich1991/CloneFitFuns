using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginSeneUI : MonoBehaviour
{
    [SerializeField] private Text nickname;
    [SerializeField] private Button confirmButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        if (nickname.text == String.Empty)
        {
            Managers.UI.ConfirmWindow("닉네임을 입력해주세요", transform);
            return;
        }
        else
        {
            Managers.UI.WarningWindow($"게임을 시작합니다!", transform, false, () =>
            {
                Managers.Player.Nickname = nickname.text;
                Managers.Scene.LoadScene(Define.Scene.Game);
            });
        }
    }
}
