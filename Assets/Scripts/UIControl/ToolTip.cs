using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _tooltip;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.SetActive(false);
    }
}
