using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private Text entryNameText;
    [SerializeField] private Text entryScoreText;

    public void Init(ScoreboardEntryData scoreboardEntryData)
    {
        entryNameText.text = scoreboardEntryData.entryName;
        entryScoreText.text = scoreboardEntryData.entryScore.ToString();
    }

}
