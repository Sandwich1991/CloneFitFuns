using UnityEngine;
using UnityEngine.UI;

public class WebBoardButton : MonoBehaviour
{
    private const string WebBoardUIPath = "WebBoardUI";
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() =>
        {
            Managers.Resource.Instantiate(WebBoardUIPath, Game.MainUI);
        });
    }
}
