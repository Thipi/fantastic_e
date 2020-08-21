using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    [SerializeField]
    Image playerAvatar;

    [SerializeField]
    Sprite[] images;

    PlayerData playerData;

    [SerializeField]
    GameObject[] playerCharacter;

    [SerializeField]
    Text playerName;

    private void Start()
        {
        playerName.text = "Player Name";
        playerAvatar.sprite = images[2];
        foreach (GameObject character in playerCharacter)
            {
            character.SetActive(false);
            }

        LoadingData loadingData = FindObjectOfType<LoadingData>();

        playerCharacter[0].SetActive(true);
        }

    public void ChooseAvatar( int avatar )
        {
        playerAvatar.sprite = images[avatar];
        
        PlayerPrefs.SetInt("PlayerImage", avatar);
        foreach (GameObject character in playerCharacter)
            {
            character.SetActive(false);
            }
        playerCharacter[avatar].SetActive(true);
        }

    public void ConfirmAndSaveImageSelection()
        {
        //save chosen character
        LoadingData loadingData = FindObjectOfType<LoadingData>();
        loadingData.chosenCharacter = PlayerPrefs.GetInt("PlayerImage");
        }

    public void ConfirmAndSavePlayerName(Text playerName)
        {
        //Saving to system. TODO: THIS ALSO NEEDS TO BE LATER ADDED TO WEB-LOGIC!!!
        string playersname = playerName.text.ToString();
        Debug.Log("name: " + playersname);

        LoadingData loadingData = FindObjectOfType<LoadingData>();
        loadingData.playerName = playersname;
        loadingData.xp = 0;
        loadingData.level = 1;
        }
    }
