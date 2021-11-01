using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ShowScoreText();
        ShowHighscoreText();
    }

    private void OnDisable()
    {
        Instance = null;
    }

    public void ShowScoreText()
    {
        scoreText.text = PlayerData.SCORE.ToString();
    }

    public void ShowHighscoreText()
    {
        highscoreText.text = PlayerData.HIGHSCORE.ToString();
    }
}
