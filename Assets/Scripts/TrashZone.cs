using UnityEngine;
using UnityEngine.EventSystems;

public class TrashZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData e)
    {
        if (e.pointerDrag != null && e.pointerDrag.GetComponent<DraggableCard>() != null)
            Destroy(e.pointerDrag);
    }
}