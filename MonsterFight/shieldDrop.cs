using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldDrop : MonoBehaviour
{
    [SerializeField]
    GameObject explosionParticles;

    AudioSource drop;

    private void Awake()
        {
        drop = GetComponent<AudioSource>();
        }

    private void OnTriggerEnter(Collider other)
        {
        if(other.tag == "DefensiveParticle") {
            GameObject puff = Instantiate(explosionParticles, other.transform.position, explosionParticles.transform.rotation);
            drop.Play();
            Destroy(other.gameObject);
            Destroy(puff, 1);
            }
        }
    }
