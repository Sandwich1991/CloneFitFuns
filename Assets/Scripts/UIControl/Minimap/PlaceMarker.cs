using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceMarker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject _toolTip;
    [SerializeField] private GameObject _warningWindow;
    [SerializeField] private Text _warningText;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancleButton;

    private Vector3 _pos = new Vector3(0, 0, -5.5f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        _toolTip.SetActive(true);
        gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        _toolTip.SetActive(false);
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _warningWindow.SetActive(true);
        _warningText.text = "(여기)로 이동합니다!";
        
        _confirmButton.onClick.AddListener(() =>
        {
            Managers.Player.PlayerPos = _pos;
            _warningWindow.SetActive(false);
        });
        
        _cancleButton.onClick.AddListener(() => _warningWindow.SetActive(false));
        
    }
}
