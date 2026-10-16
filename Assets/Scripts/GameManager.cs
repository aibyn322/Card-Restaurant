using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // lets any script call GameManager.Instance
    public TMP_Text moneyText;
    public int money;

    void Awake() { Instance = this; }
    void Start() { UpdateUI(); }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    void UpdateUI() { moneyText.text = "Money: " + money; }
}