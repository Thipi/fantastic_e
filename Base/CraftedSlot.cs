using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftedSlot : MonoBehaviour
{
    MeshRenderer rend;
    [SerializeField]
    Material newMaterial;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && other.gameObject.tag != "slot" && other.gameObject.tag != "floor" && other.gameObject.tag != "takenSlot")
        {
            Debug.Log(other.gameObject.name + " detected");
            rend.material = newMaterial;
            this.gameObject.tag = "takenSlot";
        }
    }
}