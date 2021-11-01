using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    private void OnEnable()
    {
        SavedData();
    }


    void Update()
    {
        if (PlayerData.GAME_OVER)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerData.SCORE = 0;
                ScoreManager.Instance.GetData();
                PlayerData.GAME_OVER = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void SavedData()
    {
        ScoreboardEntryData entryData = new ScoreboardEntryData
        {
            entryName = PlayerData.NAME,
            entryScore = PlayerData.SCORE
        };

        ScoreManager.Instance.AddEntry(entryData);
    }
}
