using UnityEngine;
using UnityEngine.UI;

public class WebBoardButton : MonoBehaviour
{
    private const string WebBoardUIPath = "WebBoardUI";
    private GameObject webBoard;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(() =>
        {
            webBoard = Managers.Resource.Instantiate(WebBoardUIPath, Game.MainUI);
            RectTransform rect = webBoard.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = new Vector2(1, 1);
            rect.pivot = new Vector2(0.5f, 0.5f);
        });
    }
}
