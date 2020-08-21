using Assets.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Rendering : MonoBehaviour
{
    private string nameOfObj;

    [HideInInspector]
    public float amountOfWater = 0;

    [HideInInspector]
    public float amountOfHeat = 0;

    [HideInInspector]
    public float amountOfElectricity = 0;

    [SerializeField]
    private Transform[] pivotPos;

    [HideInInspector]
    public GameObject[] visualizers;

    private int selectedVisualization;

    [SerializeField]
    private Button sahko, lampo, vesi;

    [SerializeField]
    private Text nameOfTheBuilding;

    [SerializeField]
    private TMPro.TextMeshPro[] typeAndAmount = new TMPro.TextMeshPro[3];

    private string path;

    [HideInInspector] //For visualizations:
    public string type;

    private void Start() // This seems hacky...? :thinking:
        {
        //HERE GET THE DATA  FROM JSON, BACKEND, ETC. THIS IS WHERE THE OBJECT IS RENDERED!!!
        /*var buildings = new List<Building>();*/

        nameOfObj = PlayerPrefs.GetString("name");
        amountOfWater = PlayerPrefs.GetFloat("amountOfWater");
        amountOfHeat = PlayerPrefs.GetFloat("amountOfHeat");
        amountOfElectricity = PlayerPrefs.GetFloat("amountOfElectricity");

        selectedVisualization = PlayerPrefs.GetInt("visu");
        AnalyticsClient.SendAnalytics("Entering the AR-Scene. Visualization ID: " + selectedVisualization);

        foreach (TMPro.TextMeshPro item in typeAndAmount)
        {
            item.enabled = false;
        }
        typeAndAmount[selectedVisualization].enabled = true;

        Debug.Log("The name of object is: " + nameOfObj);
        Debug.Log("Electricity: " + amountOfElectricity);
        pickVisualizer(0);

        sahko.onClick.AddListener(delegate { pickVisualizer(0); type = "electricity"; });
        lampo.onClick.AddListener(delegate { pickVisualizer(1); type = "heat"; });
        vesi.onClick.AddListener(delegate { pickVisualizer(2); type = "water"; });
        nameOfTheBuilding.text = nameOfObj;
    }

    private void pickVisualizer(int index)
    {
        PlayerPrefs.SetInt("visuAmount" + selectedVisualization + type, PlayerPrefs.GetInt("visuAmount"));
        foreach (Transform item in pivotPos[selectedVisualization])
        {
            Destroy(item.gameObject);
        }
        switch (index)
        {
            case 0:
                path = "visualizers/" + selectedVisualization + "/sahko";
                visualizers = Resources.LoadAll<GameObject>(path);
                InstantiateObject(visualizers[0], "Sähkön kulutus: ", " kWh", amountOfElectricity);
                AnalyticsClient.SendAnalytics("Watching visualization about electricity with the visualization: " + selectedVisualization);
                break;

            case 1:
                path = "visualizers/" + selectedVisualization + "/lampo";
                visualizers = Resources.LoadAll<GameObject>(path);
                InstantiateObject(visualizers[0], "Lämpö: ", " °C", amountOfHeat);
                AnalyticsClient.SendAnalytics("Watching visualization about heat with the visualization: " + selectedVisualization);
                break;

            case 2:
                path = "visualizers/" + selectedVisualization + "/vesi";
                visualizers = Resources.LoadAll<GameObject>(path);
                InstantiateObject(visualizers[0], "Veden kulutus: ", " dm3/vuosi", amountOfWater);
                AnalyticsClient.SendAnalytics("Watching visualization about water with the visualization: " + selectedVisualization);
                break;

            default:
                break;
        }
    }

    void InstantiateObject(GameObject visu, string type, string amountType, float amount)
        {
        typeAndAmount[selectedVisualization].text = type + amount + amountType;

        if (selectedVisualization == 2 && type == "Sähkön kulutus: ")
        {
            Vector3 tweakPos = new Vector3(pivotPos[selectedVisualization].position.x, pivotPos[selectedVisualization].position.y - 3, pivotPos[selectedVisualization].position.z + 5);
            GameObject tempGO = Instantiate(visu, tweakPos, visu.transform.rotation);
            tempGO.tag = "visuRendered";
            tempGO.transform.SetParent(pivotPos[selectedVisualization]);
        }
        else if (selectedVisualization == 0 && type == "Veden kulutus: ")
        {
            Vector3 tweakPos = new Vector3(pivotPos[selectedVisualization].position.x, pivotPos[selectedVisualization].position.y - 4, pivotPos[selectedVisualization].position.z + 3);
            GameObject tempGO = Instantiate(visu, tweakPos, visu.transform.rotation);
            tempGO.tag = "visuRendered";
            tempGO.transform.SetParent(pivotPos[selectedVisualization]);
        }
        else
        {
            GameObject tempGO = Instantiate(visu, pivotPos[selectedVisualization].position, visu.transform.rotation);
            tempGO.tag = "visuRendered";    //not found?
            tempGO.transform.SetParent(pivotPos[selectedVisualization]);
        }
    }
}
