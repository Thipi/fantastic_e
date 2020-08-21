using Assets.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementForCraftables : MonoBehaviour
{
    ZoomToRoom zoomToRoom;

    [HideInInspector]
    public GameObject craftedItem;
    [HideInInspector]
    string nameOfObject;

    bool zoomToRoomoinPlacementMode;

    private void Awake()
    {
        zoomToRoom = FindObjectOfType<ZoomToRoom>();
        if( PlayerPrefs.GetInt("RESETTED") == 0 )
            {
            PlayerPrefs.DeleteKey("index");
            
            }
        

    }

    private void Update()
    {
        ZoomToRoom zoom = FindObjectOfType<ZoomToRoom>();
        if(zoom.inPlacementMode)
            {
            zoomToRoomoinPlacementMode = true;
            }
        else
            {
            zoomToRoomoinPlacementMode = false;
            }

        if (zoomToRoomoinPlacementMode)
        {
#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out RaycastHit hit, 100 ))
                    {
                        if(hit.collider.tag == "slot")
                        {
                            Vector3 placementPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 0.5f, hit.collider.transform.position.z);
                            GameObject placedCrafteble = Instantiate(craftedItem, placementPosition, craftedItem.transform.rotation);

                            //For crafted items own menu: rotate/delete/recycle/upgrade/etc.
                            placedCrafteble.AddComponent<CraftableOwnMenu>();
                            placedCrafteble.AddComponent<BoxCollider>();
                            placedCrafteble.tag = "placedItem";

                            CraftableOwnMenu craftableOwnMenu = placedCrafteble.GetComponent<CraftableOwnMenu>();

                            zoomToRoom.PlacementDone();

                            PlacedItemsList placedItemsList = FindObjectOfType<PlacedItemsList>();

                            //TODO: THE ITEM IS NOW STORED TO SYSTEM, LOADING STILL NEEDS DOING.
                            placedCrafteble.AddComponent<PlacedItemData>();
                            PlacedItemData placedItemData = placedCrafteble.GetComponent<PlacedItemData>();
                            placedItemData.positionPlaced[0] = placedCrafteble.transform.position.x;
                            placedItemData.positionPlaced[1] = placedCrafteble.transform.position.y;
                            placedItemData.positionPlaced[2] = placedCrafteble.transform.position.z;
                            placedItemData.eulerAngle = placedCrafteble.transform.eulerAngles.y;
                            placedItemData.eulerX = placedCrafteble.transform.eulerAngles.x;
                            placedItemData.eulerZ = placedCrafteble.transform.eulerAngles.z;

                            int index = PlayerPrefs.GetInt("index");

                            placedItemData.name = craftedItem.name + index;
                            placedItemData.SaveItemPlaced();
                            placedItemsList.AddItemToList(craftedItem.name);
                            StoreTheString(craftedItem.name, index);
                            }
                    }
                }
            }
#endif
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    if (hit.collider.tag == "slot")
                    {
                        Vector3 placementPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y - 0.5f, hit.collider.transform.position.z);
                        GameObject placedCrafteble = Instantiate(craftedItem, placementPosition, craftedItem.transform.rotation);

                        //For crafted items own menu: rotate/delete/recycle/upgrade/etc.
                        placedCrafteble.AddComponent<CraftableOwnMenu>();
                        placedCrafteble.AddComponent<BoxCollider>();
                        placedCrafteble.tag = "placedItem";

                        CraftableOwnMenu craftableOwnMenu = placedCrafteble.GetComponent<CraftableOwnMenu>();

                        zoomToRoom.PlacementDone();

                        PlacedItemsList placedItemsList = FindObjectOfType<PlacedItemsList>();

                        //TODO: THE ITEM IS NOW STORED TO SYSTEM, LOADING STILL NEEDS DOING.
                        placedCrafteble.AddComponent<PlacedItemData>();
                        PlacedItemData placedItemData = placedCrafteble.GetComponent<PlacedItemData>();
                        placedItemData.positionPlaced[0] = placedCrafteble.transform.position.x;
                        placedItemData.positionPlaced[1] = placedCrafteble.transform.position.y;
                        placedItemData.positionPlaced[2] = placedCrafteble.transform.position.z;
                        placedItemData.eulerAngle = placedCrafteble.transform.eulerAngles.y;
                        placedItemData.eulerX = placedCrafteble.transform.eulerAngles.x;
                        placedItemData.eulerZ = placedCrafteble.transform.eulerAngles.z;

                        int index = PlayerPrefs.GetInt("index");

                        placedItemData.name = craftedItem.name + index;
                        placedItemData.SaveItemPlaced();
                        placedItemsList.AddItemToList(craftedItem.name);
                        StoreTheString(craftedItem.name, index);
                    }
                }
            }
#endif
        }
    }

    void StoreTheString(string name, int index)
        {
        PlayerPrefs.SetString("item" + index, name);
        }
    }
