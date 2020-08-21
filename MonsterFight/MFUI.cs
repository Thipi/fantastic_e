using UnityEngine;
using UnityEngine.UI;

/* TODO docs for MFUI
 * What is MFUI??
 */

public class MFUI : MonoBehaviour
{
    public Image monsterImage;

    [SerializeField]
    private GameObject UIPanel;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private GameObject winPanel;

    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private GameObject coins;

    [SerializeField]
    private GameObject particles;

    [SerializeField]
    private GameObject targetingInterface;

    AutoPlacement placeMonster;

    //For tutorial only
    [SerializeField]
    GameObject tutorialStartPanel;

    private void Awake()
    {
        particles.SetActive(false);

        // Setting up the starting conditions
        startPanel.SetActive(true);

        string tutorial = PlayerPrefs.GetString("TutorialMF");
        if(tutorial == "Monster fight tutorial mode")
            {
            tutorialStartPanel.SetActive(true);
            }
        else
            {
            Time.timeScale = 1;
            }
        // Ending conditions to hold
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        placeMonster = FindObjectOfType<AutoPlacement>();
    }

    // When Pressed GO
    public void StartTheGame()
    {
        string tutorial = PlayerPrefs.GetString("TutorialMF");
        if (tutorial != "Monster fight tutorial mode" && Time.timeScale < 1)
            {
            Time.timeScale = 1;
            }
        placeMonster.BeginTheGame();
        startPanel.SetActive(false);
        UIPanel.SetActive(false);
    }

    // After losing
    public void YouLostTheGame()
    {
        UIPanel.SetActive(true);
        losePanel.SetActive(true);
        targetingInterface.SetActive(false);
    }

    // After winning
    public void YouWonTheGame()
    {
        UIPanel.SetActive(true);
        winPanel.SetActive(true);
        particles.SetActive(true);
        targetingInterface.SetActive(false);
    }

    // Called from EnemyHealth.
    public void InCaseWeWonActivateCoinFlow(int amountOfCoinsGained)
    {
        for (int i = 0; i < amountOfCoinsGained; i++)
        {
            GameObject spawnedCoins = Instantiate(coins, monsterImage.transform.position, Quaternion.identity);
            spawnedCoins.transform.SetParent(transform);
        }

        foreach (Transform item in transform)
        {
            Destroy(item.gameObject, 0.2f);
        }
        Debug.Log("coins gathered.");
    }
}
