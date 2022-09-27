using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HomeMarker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private readonly Vector3 _pos = new Vector3(0f, 0f, -30f);
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1.0f, 0.1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.UI.WarningWindow("여기로 이동할까요?", Game.MainUI, false, () =>
        {
            Managers.Player.PlayerPos = _pos;
        });
    }
}
