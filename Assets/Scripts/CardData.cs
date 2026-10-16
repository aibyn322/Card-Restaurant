using UnityEngine;

[CreateAssetMenu(menuName = "Cafe/Card Data")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite icon;

    public int sellPrice = 1;   // what a customer pays for this item
}