using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        PlayerData.SCORE = 0;
        PlayerData.GAME_OVER = false;

        ScoreManager.Instance.GetFirstData();
        MenuUI.Instance.ShowLastName();
    }

    public void Play(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SavedLastName()
    {
        ScoreManager.Instance.SavedLastName();
    }
}
