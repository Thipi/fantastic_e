using Assets.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlacedItemsList : MonoBehaviour
{
    [HideInInspector]
    public string[] nameOfItem;

    int listIndex;

    private void Start()
        {
        if (PlayerPrefs.GetInt("RESETTED") == 0)
            {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("RESETTED", 1);
            }

        nameOfItem = new string[PlayerPrefs.GetInt("index")];

        for (int i = 0; i < nameOfItem.Length; i++) 
        {
            nameOfItem[i] = PlayerPrefs.GetString("item" + i);
            if (nameOfItem[i] != null || nameOfItem[i] != "")
                {
                UnityEngine.Object loadPrefab = Resources.Load("AllCraftablePrefabs/" + nameOfItem[i], typeof(GameObject));

                if (loadPrefab != null)
                    {
                    GameObject newInstanceOfItem = loadPrefab as GameObject;
                    newInstanceOfItem.AddComponent<PlacedItemData>();
                    PlacedItemData placedItemData = newInstanceOfItem.GetComponent<PlacedItemData>();
                    placedItemData.name = nameOfItem[i] + i;
                    placedItemData.LoadItemData();

                    //Position of the item
                    Vector3 spawnPos = new Vector3(placedItemData.positionPlaced[0], placedItemData.positionPlaced[1], placedItemData.positionPlaced[2]);
                    Debug.Log(spawnPos + " is position of item: " + nameOfItem[i]);

                    //Spawning the item
                    GameObject craftedItem = Instantiate(newInstanceOfItem, spawnPos, transform.rotation);

                    //Rotating the item
                    craftedItem.transform.eulerAngles = new Vector3(placedItemData.eulerX, placedItemData.eulerAngle, placedItemData.eulerZ);

                    AnalyticsClient.SendAnalytics("Players item spawned at Base-scene opening to the Base. Item: " + craftedItem.name);

                    if (craftedItem.GetComponent<CraftableOwnMenu>() == null)
                        {
                        craftedItem.AddComponent<CraftableOwnMenu>();
                        craftedItem.AddComponent<BoxCollider>();
                        craftedItem.tag = "placedItem";

                        CraftableOwnMenu craftableOwnMenu = craftedItem.GetComponent<CraftableOwnMenu>();
                        craftableOwnMenu.myIndex = i;
                        }
                    }
                else
                    {
                    PlayerPrefs.DeleteKey("item" + i);
                    }
                }
            }
        }

    private void Update()
        {
        if (Input.GetKey(KeyCode.D))
            {
            Debug.Log("Ready to remove everything");
            PlayerPrefs.DeleteAll();
            SaveItemToSystem.DeleteAllDataFromCraftedItem();

            foreach (GameObject item in GameObject.FindGameObjectsWithTag("placedItem"))
                {
                Destroy(item);
                }
            string s = Application.persistentDataPath + "/crafteditem.";
            Debug.Log("Removed. Amount of content in persistent datapath: " + s.Length);
            
            }
        }

    public void AddItemToList( string name )
    {
        int i = PlayerPrefs.GetInt("index");
        PlayerPrefs.SetString("item" + i, name);
        AnalyticsClient.SendAnalytics("Amounht of items player has on initialization of Base: " + i);
        i++;
        PlayerPrefs.SetInt("index", i);
        }

    internal void DeleteItemFromList(int index)
        {
        //PlayerPrefs.DeleteKey("item" + index);
        //PlayerPrefs.Save();
    }
}

