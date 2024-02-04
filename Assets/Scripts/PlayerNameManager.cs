using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;

    private void Start()
    {
        string storedName = PlayerPrefs.GetString("PlayerName", "Anon");
        playerNameInput.text = storedName;
    }

    public void SavePlayerName()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Anon";
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }
}

