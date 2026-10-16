using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IDropHandler
{
    public Image patienceBar;
    public TMP_Text orderLabel;
    public float patience = 30f;

    CardData order;
    float timeLeft;
    bool left;

    public void Init(CardData wanted)
    {
        order = wanted;
        timeLeft = patience;
        orderLabel.text = wanted.cardName;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        patienceBar.fillAmount = timeLeft / patience;
        if (timeLeft <= 0f) Leave();
    }

    public void OnDrop(PointerEventData e)
    {
        if (left || e.pointerDrag == null) return;
        var view = e.pointerDrag.GetComponent<CardView>();
        if (view == null) return;

        if (view.data == order)            // correct item delivered
        {
            GameManager.Instance.AddMoney(order.sellPrice);
            Destroy(view.gameObject);
            Leave();
        }
        // wrong item: nothing happens, the card just stays where it was dropped
    }

    void Leave()
    {
        left = true;
        Destroy(gameObject);
    }
}