using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingWithMonster : MonoBehaviour
{
    [SerializeField]
    GameObject tutMonster;

    [SerializeField]
    GameObject spawnEffect;

    [SerializeField]
    GameObject tutSpeechBubble;

    [SerializeField]
    Text tutText;

    [HideInInspector]
    public bool canTapThingsOnMap;

    [SerializeField]
    GameObject lowerMenuBar;

    [SerializeField]
    GameObject FingerPoint;

    private void Start()
        {
        LoadingData loadingData = FindObjectOfType<LoadingData>();

        if (loadingData.TutorialDone == 1)
            {
            tutMonster.SetActive(true);
            tutSpeechBubble.SetActive(false);
            }
        }

    public void StartTutorialWithMonster()
        {
        tutMonster.SetActive(true);
        GameObject puffff = Instantiate(spawnEffect, tutMonster.transform.position, tutMonster.transform.rotation);
        }

    public void MascotMonsterTapped( string textInBubble )
        {
        if(!tutSpeechBubble.activeInHierarchy)
            {
            tutSpeechBubble.SetActive(true);
            lowerMenuBar.SetActive(false);
            }
        tutText.text = textInBubble;
        }
}
