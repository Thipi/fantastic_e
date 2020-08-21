using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable()]
public class StoreObjSerialized
{
    public string nameOfObj;
    public float amountOfWater;

    public StoreObjSerialized(string nameOfObj, float amountOfWater)
        {
        this.nameOfObj = nameOfObj;
        this.amountOfWater = amountOfWater;
        }
}
