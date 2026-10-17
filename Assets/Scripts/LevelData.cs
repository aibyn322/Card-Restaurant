using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int moneyToReach;            // total money needed to enter this level
    public List<CardData> newOrders;    // drinks customers can now ask for
    public List<Recipe> newRecipes;     // recipes the Chef can now make
}