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

        e.pointerDrag = copy;  // from now on, Unity treats the COPY as the dragged object
        ExecuteEvents.Execute(copy, e, ExecuteEvents.beginDragHandler);
    }

    public void OnDrag(PointerEventData e) { }  // required so Unity starts a drag, but the copy does the moving
}