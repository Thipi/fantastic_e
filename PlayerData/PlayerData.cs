using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
    {
    [HideInInspector]
    public string playerName;

    [HideInInspector]
    public int chosenCharacter;

    [HideInInspector]
    public int level;

    [HideInInspector]
    public int xp;

    [HideInInspector]
    public int baseSelection;

    [HideInInspector]
    public float[] coordinates = new float[3];

    [HideInInspector]
    public int TutorialDone;

    public void SavePlayerData()
        {
        SavingPlayer.SavePlayerDataToSystem(this);
        }

    public void LoadPlayerData()
        {
        SavePlayerData playerData = SavingPlayer.LoadPlayerData(this);
        playerName = playerData.playerName;
        chosenCharacter = playerData.chosenCharacter;
        level = playerData.level;
        xp = playerData.xp;
        baseSelection = playerData.baseSelection;
        coordinates = playerData.coordinates;
        TutorialDone = playerData.TutorialDone;
        }

    public void DeletePlayerData()
        {
        SavingPlayer.DeleteAllPlayerData();
        }
    }
