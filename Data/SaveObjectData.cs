using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObjectData
    {
    [HideInInspector]
    public string nameOfObj;

    [HideInInspector]
    public float amountOfWater;

    public SaveObjectData(StoreObjSerialized storeObject)
        {
        nameOfObj = storeObject.nameOfObj;
        amountOfWater = storeObject.amountOfWater;
        }
    }
