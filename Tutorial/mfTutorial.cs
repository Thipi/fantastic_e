using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mfTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialPanel;

    [SerializeField]
    GameObject[] startButtons = new GameObject[2];

    [SerializeField]
    public Text tutorialBubble;

    void Start()
    {
        if (PlayerPrefs.GetString("TutorialMF").Equals("Monster fight tutorial mode"))
            {
            foreach (GameObject item in startButtons)
                {
                item.SetActive(false);
                }
            tutorialPanel.SetActive(true);
            }
    }

    public void SwitchTextBubble( Text nextBubble )
        {
        tutorialBubble = nextBubble;
        }

    public void TutorialBubble( string instruction )
        {
        tutorialBubble.text = instruction;
        }

    public void ContinueGame()
        {
        Time.timeScale = 1;
        }
}
