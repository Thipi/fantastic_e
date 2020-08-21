using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlacedData
{
    [HideInInspector]
    public string name;
    [HideInInspector]
    public float[] position;
    [HideInInspector]
    public float eulerAngle;
    public float eulerX;
    public float eulerZ;

    public SavePlacedData(PlacedItemData placedItemData)
    {
        name = placedItemData.name;

        //Position values
        position = new float[3];
        position[0] = placedItemData.positionPlaced[0];
        position[1] = placedItemData.positionPlaced[1];
        position[2] = placedItemData.positionPlaced[2];

        //Rotation values
        eulerAngle = placedItemData.eulerAngle;
        eulerX = placedItemData.eulerX;
        eulerZ = placedItemData.eulerZ;
        }
    }
