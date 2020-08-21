using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHandler : MonoBehaviour
{
    [HideInInspector]
    public int width;

    [HideInInspector]
    public int depth;

    [SerializeField]
    GameObject freeSlotIndicator;

    [SerializeField]
    GameObject[] floorPivotForSlots;

    [SerializeField]
    Quaternion rotationForCube;

    //Tässä hyvät ystävät on jesarielementti (Eli tämä pitää muuttaa oikeasti johonkin muuhun at some point):
    [SerializeField]
    Transform[] pivotForInstantiatedCubes = new Transform[6];

    private void Start()
    {
        width = 10;
        depth = 10;
    }

    public void ShowFreeSlots()
    {
        //Täs ois tätä jesarin jatkoa (Jesari pitää siis muuttaa johonkin muuhun jossain kohtaa.)
        int pivotIndex = 0;
        //Loop that iterates through the floorpivot objects, loop for x-axis, loop for z-axis and instantiating the free slots.
        foreach (GameObject item in floorPivotForSlots)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Vector3 posForFreeSlot = new Vector3(item.transform.position.x - x, item.transform.position.y, item.transform.position.z + z);
                    GameObject freeslot = Instantiate(freeSlotIndicator, posForFreeSlot, Quaternion.identity);
                    freeslot.transform.SetParent(pivotForInstantiatedCubes[pivotIndex]);

                }
            }
            pivotIndex++;
        }
    }

    public void StopFreeSlotting()
    {
        int indexForPivot = 0;
        foreach (Transform item in pivotForInstantiatedCubes)
        {
            foreach (Transform cube in pivotForInstantiatedCubes[indexForPivot])
            {
                Destroy(cube.gameObject);
            }
            indexForPivot++;
        }
    }
}
