using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlacedItemData : MonoBehaviour
{
    [HideInInspector]
    public string name;
    [HideInInspector]
    public float[] positionPlaced = new float[3];
    [HideInInspector]
    public float eulerAngle;
    [HideInInspector]
    public float eulerX;
    [HideInInspector]
    public float eulerZ;

    public void SaveItemPlaced()
    {
        Debug.Log("Storing the following data on item: " + name);
        Debug.Log("POSITION: " + positionPlaced[0] + " " + positionPlaced[1] + " " + positionPlaced[2]);
        SaveItemToSystem.SaveItem(this);
    }

    public void LoadItemData()
        {
        SavePlacedData savePlacedData = SaveItemToSystem.LoadItemData(this);
        Debug.Log(positionPlaced[0] + "POSITION HERE HALÅ");
        positionPlaced[0] = savePlacedData.position[0];
        positionPlaced[1] = savePlacedData.position[1];
        positionPlaced[2] = savePlacedData.position[2];

        eulerAngle = savePlacedData.eulerAngle;
        eulerX = savePlacedData.eulerX;
        eulerZ = savePlacedData.eulerZ;
        }

    public void RemoveItemPlaced()
        {
        SaveItemToSystem.RemoveItemData(this);
        }

}
