using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    CanvasGroup group;

    CanvasGroup Group
    {
        get
        {
            if (group == null)
            {
                group = GetComponent<CanvasGroup>();
                if (group == null) group = gameObject.AddComponent<CanvasGroup>();
            }
            return group;
        }
    }

    public void OnBeginDrag(PointerEventData e)
    {
        Group.blocksRaycasts = false;     // lets the mouse "see through" the card to what's under it
        transform.SetAsLastSibling();     // draw on top of everything
    }

    public void OnDrag(PointerEventData e)
    {
        transform.position = e.position;
    }

    public void OnEndDrag(PointerEventData e)
    {
        Group.blocksRaycasts = true;
    }
}