using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CanvasGroup group;

    void Awake() { group = GetComponent<CanvasGroup>(); }

    public void OnBeginDrag(PointerEventData e)
    {
        group.blocksRaycasts = false;     // lets the mouse "see through" the card to what's under it
        transform.SetAsLastSibling();     // draw on top of everything
    }

    public void OnDrag(PointerEventData e)
    {
        transform.position = e.position;
    }

    public void OnEndDrag(PointerEventData e)
    {
        group.blocksRaycasts = true;
    }
}