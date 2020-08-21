using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    private readonly float maximumHealth = 50;

    [HideInInspector]
    public float healthNow;

    public GameObject[] resources;

    private EnergyMaster eMaster;
    private Scene scene;
    private MFUI mfUISkripta;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 2)
        {
            eMaster = FindObjectOfType<EnergyMaster>();
            healthNow = maximumHealth;
            mfUISkripta = FindObjectOfType<MFUI>();
        }
    }

    private void FixedUpdate()
    {
        if (scene.buildIndex == 2)
        {
            if (healthNow <= 0)
            {
                int AmountOfCoins = 0;

                for (int i = 0; i < Random.Range(5, 10); i++)
                {
                    eMaster.AddFantasticEnergy(1, false);
                }
                for (int i = 0; i < Random.Range(3, 9); i++)
                {
                    eMaster.AddNeutronEnergy(1);
                    AmountOfCoins++;
                }

                /*MonsterMaster mm = FindObjectOfType<MonsterMaster>();
                mm.AddBeatenMonster(this.gameObject);

                /* TODO is this at all needed? Not used currently
                foreach (GameObject item in resources)
                {
                    CollectingRes cr = item.GetComponent<CollectingRes>();
                    CollectedRes col = FindObjectOfType<CollectedRes>();
                    //col.AddResource(cr.indexOfItem, cr.nameOfItem);
                } */

                mfUISkripta.InCaseWeWonActivateCoinFlow(AmountOfCoins);
                mfUISkripta.YouWonTheGame();
            }
        }
    }

    public void AddDamage(float damage)
    {
        healthNow -= damage;
        Debug.Log("EnemyHealth.cs, monster health now: " + healthNow);
    }
}
