using Assets.Analytics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/* THIS SCRIPT HANDLES THE MONSTERFIGHT BATTLE.
 * Description:
 * A turn based battle logic
 * The more precise players aim is, the more defensive "currency" player gets. Player has 3 seconds to collect the "currency"
 * that has scattered around.
 * On monsters turn, it will send a series of ammos that will cause and overall damage of 10.
 * If player has managed to collect 5 defensice "currencies", the final damage will be only 5.
 */

public class BattleManager : MonoBehaviour
{
    private Monster monsterScript;

    private GameObject monsterObject;

    // Timer
    [SerializeField]
    private float timeLeft;

    private bool timerOn;

    [SerializeField]
    GameObject[] turnImages = new GameObject[2];

    #region Player vars

    #region Aiming variables

    [SerializeField]
    private GameObject targetingLine;

    [SerializeField]
    private GameObject targetingInterface;

    private Vector3 lineStartPosition;

    private float linePosition;

    [SerializeField]
    private Transform lineTarget1, lineTarget2;

    [SerializeField] // TODO define by monstertype
    private float speedForLine;

    [HideInInspector]
    public float lineDistanceFromCenter;

    private float centerSpotTargeting = 0;

    private bool playerFired;

    #endregion Aiming variables

    #region Firing variables
    [SerializeField]
    Camera arCamera;

    [SerializeField]
    private GameObject FiringButton;

    private float damageToBeAdded;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform playerFiringPivot;

    private EnemyHealth enemyHealth;

    #endregion Firing variables

    #region Defensive particle variables

    [SerializeField]
    private GameObject defensiveParticle;
    [SerializeField]
    private GameObject defensiveInteractor;

    [HideInInspector]
    public int defensiveValue;

    private bool canCollectParticles;

    [SerializeField]
    Slider shieldValue;

    [SerializeField]
    private Transform pivotForParticles;

    #endregion Defensive particle variables

    #endregion Player vars

    #region Monster vars

    [SerializeField]
    private GameObject projectileM;

    private BoxCollider playerCollider;

    [SerializeField]
    private Slider monsterHealthBar;

    private float monsterHealth = 50;

    #endregion Monster vars

    #region Audiovariables

    [SerializeField]
    private AudioClip[] soundEffect = new AudioClip[2];

    private new AudioSource audio;

    #endregion Audiovariables

    /// <summary>
    /// Tutorial Variables:
    /// </summary>
    [SerializeField]
    GameObject shieldTut;

    [SerializeField]
    GameObject tutorial2ndPanel;

    [SerializeField]
    GameObject fireButtonTutorial;
    /* TODO docs for Awake() in BattleManager
     *
     */

    private void Awake()
    {
        defensiveInteractor.SetActive(false);
        audio = GetComponent<AudioSource>();
        FiringButton.SetActive(false);
        targetingInterface.SetActive(false);
        playerCollider = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<BoxCollider>();
        playerCollider.enabled = false;
        monsterHealthBar.value = monsterHealth;
        shieldValue.value = 0;
        //centerSpotTargeting = (lineTarget1.position.x - lineTarget2.position.x) / 2;

        //Tutorial components. Used only if tutorial active.
        shieldTut.SetActive(false);
        foreach (GameObject item in turnImages)
            {
            item.SetActive(false);
            }
        }

    /* Update() contains only the timers for targeting and collecting defensive particles.
     * Never change or add to the timers. Timers use the timeLeft variable.
     */

    private void Update()
    {
        if (tutorial2ndPanel.activeInHierarchy || shieldTut.activeInHierarchy || fireButtonTutorial.activeInHierarchy)
            {
            Time.timeScale = 0;
            }
        else
            {
            Time.timeScale = 1;
            }
        monsterHealthBar.value = monsterHealth;

        // Timer for targeting
        if (timerOn)
        {
            targetingLine.SetActive(true);
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0 || playerFired)
            {
                playerFired = true;
                timerOn = false;
            }
        }

        // Timer for collecting defensive particles.
        if (canCollectParticles)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0 || pivotForParticles.childCount <= 0)
            {
                canCollectParticles = false;
                StartCoroutine(BreakBetweenActions("EndingPlayerTurn", 2f));
            }
        }
    }

    public void StartTheBattle()
    {
        // Fetching monsterscript
        monsterObject = GameObject.FindGameObjectWithTag("morko");
        monsterScript = monsterObject.GetComponent<Monster>();
        enemyHealth = monsterObject.GetComponent<EnemyHealth>();

        targetingInterface.SetActive(true);
        lineStartPosition = targetingLine.transform.position;
        if(PlayerPrefs.GetString("TutorialMF") == "Monster fight tutorial mode")
            {
            tutorial2ndPanel.SetActive(true);
            }
        StartCoroutine(betweenTurns("player", 1));
    }

    // Fixed update can be used for regular update functions
    private void FixedUpdate()
    {
        monsterHealthBar.value = monsterHealth;

        if (playerFiringPivot.childCount > 1)
        {
            foreach (Transform item in playerFiringPivot)
            {
                Destroy(item.gameObject);
            }
        }

        //TARGETING LOGIC, PLAYERSTURN
        if (!playerFired && timerOn)
        {
            FiringButton.SetActive(true);

            linePosition += speedForLine * Time.deltaTime;
            targetingLine.transform.position = Vector3.Lerp(lineTarget1.position, lineTarget2.position, Mathf.PingPong(linePosition, 1f));

            //Calculating distance
            lineDistanceFromCenter = 10 - Vector2.Distance(targetingLine.transform.position, lineStartPosition);
            if (PlayerPrefs.GetString("TutorialMF") == "Monster fight tutorial mode")
                {
                targetingLine.transform.position = lineStartPosition;
                fireButtonTutorial.SetActive(true);
                }
            }

        if (playerFired)
        {
            playerFired = false;
            PlayerFiring();
        }
    }

    //TURNS

    #region PLAYER TURN

    private void PlayersTurn()
    {
        shieldValue.value = 0;

        ///Aiming
        ///with timer: 5 seconds time.
        ///For 5 seconds, the targeting line is moving back and forth on the "radar"
        ///The closer to the middle player hits, the more strike-points they get, the more damage
        ///they inflict and the more defensive particles they get. :)
        ///
        timeLeft = 5;

        playerCollider.enabled = false;


        //Starting Targeting:
        timerOn = true;
        linePosition = 0.0f;

        ///PlayerTargeting is in FixedUpdate
        ///
    }

    //THIS IS CALLED FROM THE FIRING BUTTON
    public void PlayerReleasedTargetingAndFires()
    {
        AnalyticsClient.SendAnalytics("Player used FIRE button in Monsterfight.");
        playerFiringPivot.position = new Vector3(arCamera.transform.position.x, arCamera.transform.position.y, arCamera.transform.position.z + 0.7f);
        AndroidManager.HapticFeedback();
        playerFired = true;
    }

    private void PlayerFiring()
    {
        ///Firing
        ///Spectacular show mostly. Adding damage to Monster
        ///Calculated the distance between the centerpoint of targeting line and edges
        ///     The closer the targeting ray was, the more damage it causes to the monster.
        ///     max damage = 10, min damage = 1.
        ///     Calling AddDamage with a float value, that changes the sliders value.
        ///
        //When stops: add points for damage & defensive particles
        damageToBeAdded = 10 - (Mathf.Abs(lineDistanceFromCenter) / 10);
        defensiveValue = Mathf.RoundToInt(damageToBeAdded);

        Debug.Log("distance from center: " + Mathf.Abs(lineDistanceFromCenter));

        if (damageToBeAdded > 9.1f)
        {
            damageToBeAdded = 25;
        }
        else if (damageToBeAdded <= 0)
        {
            damageToBeAdded = 1;
        }
        damageToBeAdded = damageToBeAdded + 10;
        Debug.Log("damageToAdd: " + damageToBeAdded);
        AnalyticsClient.SendAnalytics("Player managed to enflict " + damageToBeAdded + " of damage to Monster");
        //Disable the targetline and firingbutton here.
        FiringButton.SetActive(false);
        targetingLine.SetActive(false);

        linePosition = 0.0f;
        targetingLine.transform.position = lineStartPosition;
        audio.clip = soundEffect[0];
        audio.Play();
        if (playerFiringPivot.childCount <= 0)
        {
            GameObject projectileClone = Instantiate(projectile, playerFiringPivot.position, Quaternion.identity);
            projectileClone.transform.SetParent(playerFiringPivot);
            enemyHealth.AddDamage(damageToBeAdded);
        }
        monsterHealth = enemyHealth.healthNow;
        monsterHealthBar.value = monsterHealth;
        Debug.Log("BattleManager.cs, monster health now: " + monsterHealth);
        StartCoroutine(BreakBetweenActions("PlayerDefensive", 2f));
    }

    private void PlayerDefensive()
    {
        ///Collecting Defensice Particles
        ///(Timer: 5 seconds time.)
        ///Particles scattered to a reasonable area around the monster & player.
        ///
        Debug.Log("Going on defensive");
        Debug.Log("Amount of defensive particles: " + defensiveValue);
        int amountToBeSpawn = defensiveValue;
        defensiveValue = 0;
        if(amountToBeSpawn > 8)
        {
            amountToBeSpawn = 8;
        }
        else if(amountToBeSpawn < 2)
        {
            amountToBeSpawn = 1;
        }

        if(pivotForParticles.childCount > 0)
        {
            foreach (Transform item in pivotForParticles)
            {
                Destroy(item.gameObject);
            }
        }

        //pivotForParticles.position = new Vector3(pivotForParticles.transform.position.x, pivotForParticles.transform.position.y, pivotForParticles.transform.position.z + 0.7f);

        for (int i = 0; i < amountToBeSpawn; i++)
        {
            Vector3 spawnPointForShield = new Vector3(pivotForParticles.position.x + Random.Range(-0.8f, 0.8f), pivotForParticles.position.y, pivotForParticles.position.z + Random.Range(-0.8f, 0.8f));
            GameObject particle = Instantiate(defensiveParticle, spawnPointForShield, pivotForParticles.rotation);
            particle.transform.SetParent(pivotForParticles);
            float scale = Random.Range(0.08f, 0.3f);
            particle.transform.localScale = new Vector3(scale, scale, scale);
            particle.transform.rotation = pivotForParticles.rotation;
        }
        defensiveInteractor.SetActive(true);
        if (pivotForParticles.childCount > 0)
        {
            DefensiveSelection defensiveScript = defensiveInteractor.GetComponent<DefensiveSelection>();
            defensiveScript.ReActivateDefensiveSelection(pivotForParticles);
        }
        canCollectParticles = true;
        string tutState = PlayerPrefs.GetString("TutorialMF");
        if (tutState == "Monster fight tutorial mode")
            {
            shieldTut.SetActive(true);
            PlayerPrefs.SetString("TutorialMF", "TutorialCompleted");
            PlayerPrefs.SetString("TutorialBase", "Base tutorial mode");
            }
    }

    private void EndingPlayerTurn()
    {
        Debug.Log("Ending turn, defensive value: " + defensiveValue);
        AnalyticsClient.SendAnalytics("Players collected defensive value in monsterfight: " + defensiveValue);
        foreach (Transform item in pivotForParticles)
        {
            Destroy(item.gameObject);
        }
        defensiveInteractor.SetActive(false);
        ///ENDING THE PLAYER TURN
        ///
        turnImages[1].SetActive(true);
        StartCoroutine(betweenTurns("monster", 1f));
    }

    #endregion PLAYER TURN

    #region MONSTER TURN

    private void MonstersTurn()
    {
        ///Spawning the projectiles
        ///
        monsterScript = monsterObject.GetComponent<Monster>();
        monsterScript.TriggerAnimation(); //TODO: ANIMATION NEEDS TO WORK AS A TRIGGER TO THE FIRE METHOD HERE!!! 
        Debug.Log("Starting Monsters turn");
        for (int i = 0; i < 4; i++)
        {
            GameObject cloneProj = Instantiate(projectileM, monsterObject.transform.position, Quaternion.identity);
            cloneProj.transform.SetParent(pivotForParticles, true);

            //Make a list of targets where projectiles are headed and set the i values here.
            cloneProj.transform.position = monsterScript.projectileTargetPositions[i].position;
            cloneProj.AddComponent<Projectile>();
        }
        playerCollider.enabled = true;
        StartCoroutine(BreakBetweenActions("FiringSequence", 0.1f));
    }

    private void FiringSequence()
    {
        ///Firing
        ///
        Debug.Log("Monster firing");
        foreach (Transform item in pivotForParticles)
        {
            Projectile commandAmmo = item.gameObject.GetComponent<Projectile>();
            commandAmmo.ShootTowardsPlayer();
        }
        ///Causing damage
        ///
        audio.clip = soundEffect[1];
        audio.Play();
        Debug.Log("Adding damage to player: " + (20 - defensiveValue));
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.AddDamage(20 - defensiveValue);
        ///End of Monsters turn.
        ///
        turnImages[0].SetActive(true);
        Debug.Log("Players turn");
        StartCoroutine(betweenTurns("player", 3f));
    }

    #endregion MONSTER TURN

    /// Between TURNS
    private IEnumerator betweenTurns(string whoseTurn, float time)
    {
        yield return new WaitForSeconds(time);
        switch (whoseTurn)
        {
            case "player":
                PlayersTurn();
                break;

            case "monster":
                MonstersTurn();
                break;

            default:
                break;
        }
    }

    /// Use this method in between actions to determine the required "breaktime" between all actions.
    private IEnumerator BreakBetweenActions(string nextMethod, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Invoke(nextMethod, 1f);
    }
}
