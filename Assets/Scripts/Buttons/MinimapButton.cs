using UnityEngine;
using UnityEngine.UI;

public class MinimapButton : MonoBehaviour
{
    private Button _minimapButton;
    private GameObject _minimap;
    private bool _isActive;

    private void Start()
    {
        _minimapButton = GetComponent<Button>();
        _minimapButton.onClick.AddListener(ToggleMinimap);
    }

    void ToggleMinimap()
    {
        if (!_isActive)
        {
             _minimap = Managers.Resource.Instantiate("Minimap", Game.MainUI);
             RectTransform rect = _minimap.GetComponent<RectTransform>();
             rect.anchoredPosition = new Vector2(40, -40);
        }
        else
        {
            Managers.Resource.Destroy(_minimap);
        }

        _isActive = !_isActive;
    }
}
