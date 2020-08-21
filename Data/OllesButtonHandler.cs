using Assets.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OllesButtonHandler : MonoBehaviour
{
    public Button OllesButton;

    // Start is called before the first frame update
    //public void Start()
    //{
    //    OllesButton.onClick.AddListener(Test);
    //}

    //public void Test()
    //{
    //    var bdings = new List<Building>();
    //    Debug.Log("Olle debug: Olles test button clicked!");
    //    try
    //    {
    //        DataFetcher.GetBuildings((buildings) =>
    //        {
    //            if (buildings != null)
    //            {
    //                foreach (Building b in buildings)
    //                {
    //                    Debug.Log("Olle: A BUILDING - SYNCHRONOUSLY: " + b.Name);
    //                }
    //                Debug.Log("Olle: Attempting to get a data value...");
    //                DataFetcher.GetData(
    //                    buildings[0],
    //                    new DateTime(2020, 3, 21, 12, 0, 0),
    //                    (data) =>
    //                    {
    //                        if (data != null)
    //                        {
    //                            DateTime start = new DateTime(2020, 3, 21, 0, 0, 0);
    //                            DateTime end = start.AddHours(23);
    //                            Debug.Log("Olle debug: A DATA VALUE - SYNCHRONOUSLY: " + data.Value);
    //                        }
    //                    }
    //                );
    //            }
    //        });
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.Log("Olle debug: Failed to get data due to " + e.ToString().Replace("\n", ". "));
    //    }
    //}
}
