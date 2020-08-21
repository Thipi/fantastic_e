using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTutMap : MonoBehaviour
{
    //Let's remove all traces of the previous tutorial first.
    [SerializeField]
    GameObject mascot;
    [SerializeField]
    GameObject fingerPoint;

    [SerializeField]
    GameObject baseTutorialCanvas;

    int baseSelected;

    [HideInInspector]
    public string statusOfTut;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        float value = playerData.coordinates[0] + playerData.coordinates[1] + playerData.coordinates[2];
        string statusOfTut = PlayerPrefs.GetString("TutorialBase");
        if ( value == 0  && statusOfTut == "Base tutorial mode")
            {
            baseTutorialCanvas.SetActive(true);
            mascot.tag = "morko";
            if (fingerPoint.activeInHierarchy)
                {
                Destroy(fingerPoint);
                }
            }
        else
            {
            baseTutorialCanvas.SetActive(false);
            }
    }

    public void BaseSelection(int selected)
        {
        baseSelected = selected;
        }

    public void ConfirmAndSave()
        {
        LoadingData loadingData = FindObjectOfType<LoadingData>();
        loadingData.baseSelection = baseSelected;
        }
}
