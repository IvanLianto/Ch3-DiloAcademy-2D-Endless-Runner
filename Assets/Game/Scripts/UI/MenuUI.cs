using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance;
    [SerializeField] private InputField nameInput;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowLastName()
    {
        if (PlayerData.NAME != "") nameInput.text = PlayerData.NAME;
    }

    public void SavedName()
    {
        if (nameInput.text.Trim() == "")
        {
            PlayerData.NAME = "Budi";
        }
        else
        {
            PlayerData.NAME = nameInput.text;
        }
    }
}
