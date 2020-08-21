using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerData
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

    public SavePlayerData(PlayerData playerData)
        {
        playerName = playerData.playerName;
        chosenCharacter = playerData.chosenCharacter;
        level = playerData.level;
        xp = playerData.xp;
        baseSelection = playerData.baseSelection;
        coordinates = playerData.coordinates;
        TutorialDone = playerData.TutorialDone;
        }
    }
