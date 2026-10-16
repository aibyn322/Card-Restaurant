using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cafe/Recipe")]
public class Recipe : ScriptableObject
{
    public List<CardData> ingredients;
    public CardData result;
    public float craftTime = 3f;
}