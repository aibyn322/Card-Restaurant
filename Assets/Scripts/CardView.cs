using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    public CardData data;
    [SerializeField] TMP_Text title;
    [SerializeField] Image icon;

    void Start()
    {
        if (data != null) Setup(data);
    }

    public void Setup(CardData d)
    {
        data = d;
        title.text = d.cardName;
        icon.sprite = d.icon;
    }
}