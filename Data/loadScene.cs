﻿using Assets.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public void BackToMainMenu()
        {
        //AnalyticsClient.SendAnalytics("Returning to menu from visualization: " + PlayerPrefs.GetInt("visu").ToString());
        SceneManager.LoadScene(1);
        }
}
