using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class EditorCamera : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            {
            PlayerPrefs.DeleteAll();
            }

        if (Input.GetKey("up"))
        {
            transform.Translate(Vector3.up);
        }
        if (Input.GetKey("down"))
        {
            transform.Translate(Vector3.down);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(Vector3.left);
        }
        if (Input.GetKey("right"))
        {
            transform.Translate(Vector3.right);
        }

        if (Camera.main.orthographicSize > 8.2f) {
            if (Input.GetKey("w"))
            {
                Camera.main.orthographicSize--;
            }
        }
        if (Camera.main.orthographicSize < 18) {
            if (Input.GetKey("s"))
            {
                Camera.main.orthographicSize++;
            }
        }

        if (Input.GetMouseButtonDown(0))
            {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {

                //From the raycast data it's easy to get the vector3 of the hit point 
                Vector3 worldVector = hit.point;
                //And it's just as easy to get the gps coordinate of the hit point.
                if (hit.collider != null && hit.collider.transform.gameObject.tag.Equals("placedItem"))
                    {
                    CraftableOwnMenu craftableOwnMenu = hit.transform.gameObject.GetComponent<CraftableOwnMenu>();
                    craftableOwnMenu.SetFurnitureMenuActive();
                    }
                }
            }
    }
}
#endif
