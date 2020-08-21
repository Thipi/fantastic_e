using Assets.Analytics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStash : MonoBehaviour
{
    // Update is called once per frame
    GameObject treasureBox;

    [SerializeField]
    ParticleSystem particles;

    bool cantap = true;

    int counter = 0;

    ResourcesHandlerInScene resHandler;

    bool collecting = true;

    Animator anim;

    [SerializeField]
    GameObject arkunKansi;

    [SerializeField]
    AudioSource arkkuAani;

    [SerializeField]
    GameObject goldenParticles;
    void Awake()
        {
        anim = arkunKansi.GetComponent<Animator>();
        goldenParticles.SetActive(false);
        AnalyticsClient.SendAnalytics("Resource collection scene opened.");
        }

    void Update()
        {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && cantap && counter < 15 )
            {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                //From the raycast data it's easy to get the vector3 of the hit point 
                Vector3 worldVector = hit.point;
                //And it's just as easy to get the gps coordinate of the hit point.
                if (hit.collider != null)
                    {
                    if (hit.collider.tag == "tapMe")
                        {
                        arkkuAani.Play();
                        cantap = false;
                        treasureBox = hit.collider.transform.gameObject;
                        particles.Play();
                        counter++;
                        StartCoroutine(ScaleOverTime(0.2f));
                        }
                    }
                }
           }
#endif
#if UNITY_ANDROID
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && cantap && counter < 15)
            {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                Vector3 worldVector = hit.point;
                if (hit.collider != null)
                    {
                    if (hit.collider.tag == "tapMe")
                        {
                        arkkuAani.Play();
                        cantap = false;
                        treasureBox = hit.collider.transform.gameObject;
                        particles.Play();
                        counter++;
                        StartCoroutine(ScaleOverTime(0.2f));
                        }
                    }
                }
            }
#endif
        if ( counter >= 15 && collecting)
            {
            goldenParticles.SetActive(true);
            collecting = false;
            anim.SetTrigger("open");
            }
        }

    IEnumerator ScaleOverTime(float time)
        {
        Vector3 originalScale = treasureBox.transform.localScale;
        Vector3 destinationScale = new Vector3(0.8f, 0.8f, 0.8f);

        float counter = 0.0f;

        do
            {
            treasureBox.transform.localScale = Vector3.Lerp(originalScale, destinationScale, counter / time);
            counter += Time.deltaTime;
            yield return null;
            } while (counter <= time);

        treasureBox.transform.localScale = originalScale;
        cantap = true;
        }
    }
