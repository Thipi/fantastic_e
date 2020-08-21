using Assets.Analytics;
using Assets.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buildingslist : MonoBehaviour
{
    private List<GameObject> buttonsBuildings;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private GameObject buttonPrefab;

    private void Start()
    {
        DataFetcher.GetBuildings(BuildingsToButtons);
    }

    private void BuildingsToButtons(List<Building> buildings)
    {
        Debug.Log("Buildings from API: " + buildings.Count);
        buttonsBuildings = new List<GameObject>();
        GameObject[] resourcesArray = Resources.LoadAll<GameObject>("buildings");
        int selectingVisu = 0;
        foreach (Building b in buildings)
        {
            if(selectingVisu >= 2)
                {
                selectingVisu = 0;
                }
            var index = buildings.IndexOf(b);
            GameObject clone = resourcesArray[index];
            buttonsBuildings.Add(clone);

            GameObject newObj = Instantiate(buttonPrefab, content); // button
            newObj.transform.GetChild(0).gameObject.GetComponent<Text>().text = b.Name.ToString(); //Change this to getId/getName etc.
            newObj.AddComponent<DataHolder>();
            DataHolder dataHolder = newObj.GetComponent<DataHolder>();
            dataHolder.nameOfObj = b.Name;
            dataHolder.selectedDataVisualization = selectingVisu;
            newObj.GetComponent<Button>().onClick.AddListener(delegate { dataHolder.AddToStorageOnClick(b.Id); SceneManager.LoadScene(2); });
            selectingVisu++;
        }
    }
}
