using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingData : MonoBehaviour
{
    Scene scene;
    PlayerData playerData;

    /// <summary>
    /// MAP ///
    /// 
    /// Checking on the map if tutorial has been checked through and the player has set their playername. 
    /// If not, then initializing tutorial sequence
    /// 
    /// If the player however HAS chosen their character and started tutorial on the map:
    ///(Apply everywhere):
    ///Set playerName, xp, level, correct sprite to character and correct character model.
    /// 
    /// 
    /// NOTE: THIS SCRIPT LOADS ALL DATA AND IS THE ONLY SCRIPTS ACCESSING PLAYER DATA.
    /// In this script the data will be available to other classes.
    /// 
    /// </summary>
    GameObject tutorialCanvas;

    [SerializeField]
    Text playerNameText;

    [SerializeField]
    Slider xpSlider;

    [SerializeField]
    Text LVL;

    [SerializeField]
    Sprite[] playerImg;

    [SerializeField]
    Image playerAvatar;

    [SerializeField]
    GameObject[] playerCharacter;

    [SerializeField]
    GameObject mascot;

    [SerializeField]
    GameObject pointyFinger;

    //PLAYER DATA//
    [HideInInspector]
    public string playerName = default;

    [HideInInspector]
    public int chosenCharacter = default;

    [HideInInspector]
    public int level = default;

    [HideInInspector]
    public int xp = default;

    [HideInInspector]
    public int baseSelection = default;

    [HideInInspector]
    public float[] coordinates = new float[3];

    [HideInInspector]
    public int TutorialDone;

    private void Awake()
        {
        //Let's start by loading the data
        PlayerData playerData = FindObjectOfType<PlayerData>();
        try
            {
            playerData.LoadPlayerData();

            xp = playerData.xp;
            playerName = playerData.playerName;
            level = playerData.level;
            baseSelection = playerData.baseSelection;
            chosenCharacter = playerData.chosenCharacter;
            coordinates = playerData.coordinates;
            TutorialDone = playerData.TutorialDone;
            }
        catch
            {
            throw new System.Exception( "Data not available" );
            }

        //Then some logic for tutorial if needed:
        tutorialCanvas = GameObject.Find("TUTORIAL");

        if ( playerData.playerName == null && scene.buildIndex == 1 )
            {
            tutorialCanvas.SetActive(true);
            }
        else
            {
            playerNameText.text = playerName;
            xpSlider.value = xp;
            LVL.text = level.ToString();
            Debug.Log("Chosen character: " + playerData.chosenCharacter);
            playerAvatar.sprite = playerImg[chosenCharacter];
            foreach (GameObject character in playerCharacter)
                {
                character.SetActive(false);
                }
            playerCharacter[chosenCharacter].SetActive(true);
            tutorialCanvas.SetActive(false);
            mascot.SetActive(true);
            mascot.transform.GetChild(0).tag = "morko";
            pointyFinger.SetActive(false);
            TutorialDone = 0;
            }
        }
    void Update() //NOTE: this is ONLY for development use!! COMMENT OUT BEFORE MERGING!
        {
        if (Input.GetButton("Jump"))
            {
            Debug.Log("Removing all player Data");
            PlayerData playerData = FindObjectOfType<PlayerData>();
            playerData.DeletePlayerData();

            PlayerPrefs.DeleteAll();
            }
        }

    //Use this method when changing scene. :) 
    public void SaveData()
        {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        //Let's make sure everything is as should.
        playerData.xp = xp;
        playerData.playerName = playerName;
        playerData.level = level;
        playerData.baseSelection = baseSelection;
        playerData.chosenCharacter = chosenCharacter;
        playerData.coordinates = coordinates;
        playerData.TutorialDone = TutorialDone;

        playerData.SavePlayerData();
        }
    }
