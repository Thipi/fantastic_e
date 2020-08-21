using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefensiveSelection : MonoBehaviour
{
    GameObject[] defensiveParticles;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private float distanceFromCamera = 25.0f;
    [SerializeField]
    private float generateRayAfterSeconds = 0.2f;
    private float rayTimer = 0;
    Vector2 touchPosition = default;
    private BattleManager battleManager;

    [SerializeField]
    Slider shieldValue;

    [SerializeField]
    GameObject pickedShield;

    AudioSource pickShield;

    private void Awake()
        {
        pickShield = GetComponent<AudioSource>();
        }
    private void Update()
    {
        if (rayTimer >= generateRayAfterSeconds)
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
            {
#if UNITY_EDITOR
                touchPosition = Input.mousePosition;
#else
                touchPosition = Input.GetTouch(0).position;
#endif
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, distanceFromCamera))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == "DefensiveParticle")
                        {
                            GameObject puff = Instantiate(pickedShield, hit.transform.position, pickedShield.transform.rotation);
                            Destroy(hit.transform.gameObject);
                            pickShield.Play();
                            Destroy(puff, 0.2f);
                            AddDefensivePoints();
                        }
                    }
                }
            }
        }
        else
        {
            rayTimer += Time.deltaTime;
        }
    }

    public void ReActivateDefensiveSelection( Transform pivotForParticles )
    {
        defensiveParticles = new GameObject[pivotForParticles.childCount];
        int index = 0;
        foreach (Transform item in pivotForParticles)
        {
            defensiveParticles[index] = item.gameObject;
            index++;
        }
        Debug.Log(defensiveParticles.Length + " amount of Defensive particles");
    }

    public void AddDefensivePoints()
        {
        battleManager = FindObjectOfType<BattleManager>();
        battleManager.defensiveValue++;

        int value = battleManager.defensiveValue;
        if( value > shieldValue.maxValue )
            {
            value = (int) shieldValue.maxValue;
            }

        for (int i = 0; i <= value; i++)
            {
            shieldValue.value = i;
            }
        }
    }
