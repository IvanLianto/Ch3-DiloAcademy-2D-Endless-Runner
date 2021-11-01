using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreboardSaveData
{
    public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();
    public string lastName;
}
