using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    //This script is for transfering the necessary data from UI to AR Scene, this is temp stuff so no actual storing here.
    //This script needs to be in both scenes in order to work

    [HideInInspector]
    public string nameOfObj;

    [HideInInspector]
    public float amountOfWater;

    [HideInInspector]
    public float amountOfHeat;

    [HideInInspector]
    public float amountOfElectricity;

    [HideInInspector]
    public int selectedDataVisualization;

    public void ChosenData()
        {
        PlayerPrefs.SetFloat("amountOfWater", amountOfWater);
        PlayerPrefs.SetFloat("amountOfHeat", amountOfHeat);
        PlayerPrefs.SetFloat("amountOfElectricity", amountOfElectricity);
        PlayerPrefs.SetString("name", nameOfObj);
        PlayerPrefs.SetInt("visu", selectedDataVisualization);
        }

}
