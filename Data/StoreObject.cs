using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class StoreObject : MonoBehaviour

    {
    [HideInInspector]
    public string nameOfObj;
    [HideInInspector]
    public float amountOfWater;
    public void SaveStoredObject()
        {
        StoreObjSerialized storeObjSerialized = new StoreObjSerialized(nameOfObj, amountOfWater);
        SaveObjectToSystem.SaveItem(storeObjSerialized);
        }

    public void LoadObject(string path)
        {
        SaveObjectToSystem.LoadObjectData(path);
        }
    }
