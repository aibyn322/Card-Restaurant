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
        if (left) return;

        timeLeft -= Time.deltaTime;
        if (patienceBar != null)
            patienceBar.fillAmount = timeLeft / patience;

        if (timeLeft <= 0f)
        {
            GameManager.Instance.ChangeReputation(-0.15f);   // customer gave up
            Leave();
        }
    }

    public void OnDrop(PointerEventData e)
    {
        if (left || e.pointerDrag == null) return;
        var view = e.pointerDrag.GetComponent<CardView>();
        if (view == null) return;

        if (view.data == order)
        {
            GameManager.Instance.AddMoney(order.sellPrice);
            GameManager.Instance.ChangeReputation(+0.1f);
            Destroy(view.gameObject);
            Leave();
        }
    }
    void Leave()
    {
        if (left) return;                        // never run twice
        left = true;
        Destroy(gameObject);
    }
}