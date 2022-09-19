using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMarker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _toolTip;
    
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
}
