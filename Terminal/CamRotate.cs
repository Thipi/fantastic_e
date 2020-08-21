using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public void RotateLeft()
        {
        Camera.main.transform.Rotate(0, -30, 0);
        }

    public void RotateRight()
        {
        Camera.main.transform.Rotate(0, 30, 0);
        }
    }
