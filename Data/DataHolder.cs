using Assets.Analytics;
using Assets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [HideInInspector]
    public string nameOfObj;

    public float amountOfWater = 0;
    public float amountOfHeat = 0;
    public float amountOfElectricity;

    [HideInInspector]
    public int selectedDataVisualization = 0;

    public void AddToStorageOnClick(int id)
    {
        AnalyticsClient.SendAnalytics("User pressed building " + id);
        DataFetcher.GetEnergyDataOfOneWeek(id, DateTime.Now.AddDays(-30), SetValues);
    }

    private void SetValues(List<EnergyData> list)
    {
        #region Olles calculations

        var average = list.Average(x => x.Value);
        var now = DateTime.Now.Hour;
        var chosenPoint = list.SingleOrDefault(x => x.Hour.Hour == now);
        if (chosenPoint is null)
        {
            chosenPoint = list[(int)Math.Floor((decimal)(list.Count / 2.0))];
        }

        var diff = chosenPoint.Value / average;     // eg. 60 / 50 = 1.2
        var presentedValue = diff * 0.5;           // assuming 1.2 * 0.5 = 0.6 -> a bit higher than usual
        amountOfElectricity = (float)presentedValue;
        int degree = (int)Math.Round(presentedValue * 5, 0);     //Value from 0 to 5 ( no energy usage to very high energy usage)
        #endregion Olles calculations

        PlayerPrefs.SetInt("visuAmount", degree);
        /*this.gameObject.AddComponent<StoreObject>();
        StoreObject storeObject = this.gameObject.GetComponent<StoreObject>();
        */
        DataHandler dataTransfering = FindObjectOfType<DataHandler>(); //Move this to buildingslist script

        dataTransfering.nameOfObj = nameOfObj;
        dataTransfering.amountOfWater = amountOfWater;
        dataTransfering.amountOfHeat = amountOfHeat;
        dataTransfering.amountOfElectricity = amountOfElectricity;
        dataTransfering.selectedDataVisualization = selectedDataVisualization;
        dataTransfering.ChosenData();

        /*storeObject.nameOfObj = this.nameOfObj;
        storeObject.amountOfWater = this.amountOfWater;
        Debug.Log("In StoreObject the name of Object : " + storeObject.nameOfObj);
        if(storeObject.nameOfObj == null || storeObject.amountOfWater == 0)
            {
            storeObject.nameOfObj = "default";
            storeObject.amountOfWater = 5;
            dataTransfering.amount = 5;
            }
        storeObject.SaveStoredObject();*/
    }
}
