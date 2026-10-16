using UnityEngine;
using UnityEngine.EventSystems;

public class CardSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public CardData data;
    public GameObject cardPrefab;
    public Transform cardParent;

    public void OnBeginDrag(PointerEventData e)
{
    var copy = Instantiate(cardPrefab, e.position, Quaternion.identity, cardParent);
    copy.GetComponent<CardView>().Setup(data);

    e.pointerDrag = copy;                                  // the copy is now the dragged object
    copy.GetComponent<DraggableCard>().OnBeginDrag(e);     // call ONLY this script directly
}

    public void OnDrag(PointerEventData e) { }  // required so Unity starts a drag, but the copy does the moving
}