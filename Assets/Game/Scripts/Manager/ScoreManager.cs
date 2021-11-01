using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    [SerializeField] private int maxEntryData = 5;
    private string SavePath => $"{Application.persistentDataPath}/highscore.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores();

        savedScores.lastName = PlayerData.NAME;

        bool scoreAdded = false;

        for (var i = 0; i < savedScores.highscores.Count; i++)
        {
            if (scoreboardEntryData.entryScore > savedScores.highscores[i].entryScore)
            {
                savedScores.highscores.Insert(i, scoreboardEntryData);
                scoreAdded = true;
                break;
            }
        }

        if (!scoreAdded && savedScores.highscores.Count < maxEntryData)
        {
            savedScores.highscores.Add(scoreboardEntryData);
        }

        if (savedScores.highscores.Count > maxEntryData)
        {
            savedScores.highscores.RemoveRange(maxEntryData, savedScores.highscores.Count - maxEntryData);
        }

        SaveData(savedScores);

    }

    public ScoreboardSaveData GetSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new ScoreboardSaveData();
        }

        using (StreamReader streamReader = new StreamReader(SavePath))
        {
            string json = streamReader.ReadToEnd();
            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }
    }

    public void SaveData(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true);
            stream.Write(json);
        }
    }

    public void GetFirstData()
    {
        ScoreboardSaveData saveData = GetSavedScores();

        if (saveData.highscores.Count == 0)
        {
            PlayerData.HIGHSCORE = 0;
        }
        else
        {
            PlayerData.HIGHSCORE = saveData.highscores[0].entryScore;
        }

        if (saveData.lastName != "")
        {
            PlayerData.NAME = saveData.lastName;
        }

        SaveData(saveData);
    }

    public void GetData()
    {
        ScoreboardSaveData saveData = GetSavedScores();

        if (saveData.highscores.Count == 0)
        {
            PlayerData.HIGHSCORE = 0;
        }
        else
        {
            PlayerData.HIGHSCORE = saveData.highscores[0].entryScore;
        }
    }

    public void SavedLastName()
    {
        ScoreboardSaveData saveData = GetSavedScores();

        saveData.lastName = PlayerData.NAME;

        SaveData(saveData);
    }
}
