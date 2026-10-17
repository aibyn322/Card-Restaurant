using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text moneyText;
    public TMP_Text levelText;
    public TMP_Text goalText;
    public Image reputationBar;
    public GameObject gameOverPanel;

    [Header("Progress")]
    public List<LevelData> levels;
    public int money;
    public int level = 1;
    [Range(0f, 1f)] public float reputation = 0.5f;

    public List<CardData> UnlockedOrders = new List<CardData>();
    public List<Recipe> UnlockedRecipes = new List<Recipe>();

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;   // in case we restarted after a game over
    }

    void Start()
    {
        Unlock(levels[0]);     // level 1 content
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        CheckLevelUp();
        UpdateUI();
    }

    public void ChangeReputation(float delta)
    {
        reputation = Mathf.Clamp01(reputation + delta);
        UpdateUI();
        if (reputation <= 0f) GameOver();
    }

    void CheckLevelUp()
    {
        // 'level' is 1-based, so levels[level] is the NEXT level
        while (level < levels.Count && money >= levels[level].moneyToReach)
        {
            Unlock(levels[level]);
            level++;
        }
    }

    void Unlock(LevelData data)
    {
        UnlockedOrders.AddRange(data.newOrders);
        UnlockedRecipes.AddRange(data.newRecipes);
    }

    void GameOver()
    {
        Time.timeScale = 0f;                       // freezes the game
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        moneyText.text = "Money: " + money;
        levelText.text = "Level: " + level;
        reputationBar.fillAmount = reputation;

        if (level < levels.Count)
            goalText.text = "Earn " + (levels[level].moneyToReach - money) + "$ to reach next level.";
        else
            goalText.text = "Max level!";
    }
}