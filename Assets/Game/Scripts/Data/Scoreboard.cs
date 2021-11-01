using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Scoreboard : MonoBehaviour
{
    [SerializeField] private Transform highscoreTemplate;
    [SerializeField] private GameObject scoreboardSlot;

    private void OnEnable()
    {
        ScoreboardSaveData savedScores = ScoreManager.Instance.GetSavedScores();

        UpdateUI(savedScores);

        ScoreManager.Instance.SaveData(savedScores);
    }

    private void UpdateUI(ScoreboardSaveData savedScores)
    {
        foreach(Transform child in highscoreTemplate)
        {
            Destroy(child.gameObject);
        }

        foreach(ScoreboardEntryData highscore in savedScores.highscores)
        {
            Instantiate(scoreboardSlot, highscoreTemplate).GetComponent<ScoreboardEntryUI>().Init(highscore);
        }
    }
}
