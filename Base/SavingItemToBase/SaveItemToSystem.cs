using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Analytics;
using System.Runtime.Serialization;
using System;

public static class SaveItemToSystem
{
    public static void SaveItem(PlacedItemData placedItemData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        //Storing the item to persistent datapath under "crafteditems" with the name created and an int to avoid collisions.
        string path = Application.persistentDataPath + "/crafteditem." + placedItemData.name;
        FileStream stream = new FileStream(path, FileMode.Create);

        SavePlacedData savePlacedData = new SavePlacedData(placedItemData);

        formatter.Serialize(stream, savePlacedData);
        stream.Close();
        Debug.Log("File stored to: " + path);
        AnalyticsClient.SendAnalytics("Players item " + placedItemData.name + " successfully stored in to devices datapath.");
        }


    public static SavePlacedData LoadItemData(PlacedItemData placedItemData)
    {
        string path = Application.persistentDataPath + "/crafteditem." + placedItemData.name;
        if (File.Exists(path))
            {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlacedData data = formatter.Deserialize(stream) as SavePlacedData;

            stream.Close();
            Debug.Log("File loaded from: " + path);
            AnalyticsClient.SendAnalytics("Players item " + placedItemData.name + " successfully loaded from datapath.");
            return data;
        }
        else
        {
            Debug.Log("File not found in this location: " + path);
            return null;
        }

    }

    public static void RemoveItemData(PlacedItemData placedItemData)
        {
        string path = Application.persistentDataPath + "/crafteditem." + placedItemData.name;
        if (File.Exists(path))
            {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlacedData savePlacedData = new SavePlacedData(placedItemData);
            SavePlacedData data = formatter.Deserialize(stream) as SavePlacedData;
            stream.Dispose();
            try
                {
                File.Delete(path);
                Debug.Log("File deleted from: " + path);
                AnalyticsClient.SendAnalytics("Players item " + placedItemData.name + " successfully deleted from datapath.");
                }
            catch (Exception ex)
                {
                Debug.LogException(ex);
                }
             stream.Close();
            }
        }

    public static void DeleteAllDataFromCraftedItem()
        {
        if (File.Exists(Application.persistentDataPath + "/crafteditem."))
            {
                File.Delete(Application.persistentDataPath + "/crafteditem");
            }
        }
    }
