using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    bool isGoingUp = true;
    Rigidbody rb;

    [SerializeField]
    float speed;

    private void Awake()
        {
        rb = GetComponent<Rigidbody>();
        }

    // Update is called once per frame
    void Update()
    {
        if(isGoingUp)
            {
            rb.AddForce(new Vector3(0, speed, 0) * Time.deltaTime, ForceMode.Impulse);
            }
    }

    public void TriggerSlowdown()
        {
        rb.mass = rb.mass + 0.02f;
        speed = speed - 0.1f;
        }

    public void TriggerStop()
        {
        isGoingUp = false;
        rb.isKinematic = true;
        }

    public void TriggerFall()
        {
        rb.isKinematic = false;
        }
}
