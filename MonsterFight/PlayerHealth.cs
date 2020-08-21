using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    float maximumHealth = 70;
    float healthNow;
    [SerializeField]
    Slider playerHealthBar;
    MFUI mfUiSkript;

    // Start is called before the first frame update
    void Start()
    {
        mfUiSkript = FindObjectOfType<MFUI>();
        healthNow = maximumHealth;
        playerHealthBar.value = maximumHealth;
    }

    private void Update()
    {
        if (healthNow <= 0)
        {
            Handheld.Vibrate();
            mfUiSkript.YouLostTheGame();
        }
    }

    public void AddDamage(float damage)
    {
            healthNow = healthNow - damage;
            playerHealthBar.value = healthNow;
    }
}
