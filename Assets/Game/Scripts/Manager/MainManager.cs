using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    [SerializeField] private GameObject gameOverPanel;

    public UnityEvent onGameOver = new UnityEvent();

    private void Awake() => Instance = this;

    private void Start()
    {
        onGameOver.RemoveAllListeners();
        onGameOver.AddListener(OnGameOver);
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnGameOver()
    {
        PlayerData.GAME_OVER = true;
        gameOverPanel.SetActive(true);
    }

    #region Score
    public void AddScore(int amount)
    {
        PlayerData.SCORE += amount;
        GameUI.Instance.ShowScoreText();
    }

    public void SubstractScore(int amount)
    {
        PlayerData.SCORE -= amount;

        if (PlayerData.SCORE <= 0)
        {
            PlayerData.SCORE = 0;
        }

        GameUI.Instance.ShowScoreText();
    }
    #endregion
}
