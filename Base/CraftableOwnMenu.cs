using UnityEngine;
using UnityEngine.UI;

public class CraftableOwnMenu : MonoBehaviour
    {
    private GameObject furnitureMenu;
    private Button rotateMe, recycleMe, deleteMe, upgradeMe;
    [HideInInspector]
    public int myIndex;

    private void Start()
        {
        furnitureMenu = Resources.Load<GameObject>("furnitureMenu/furnituremenu");
        GameObject cloningMenu = Instantiate(furnitureMenu, gameObject.transform.position, gameObject.transform.rotation);
        cloningMenu.transform.SetParent(gameObject.transform);
        rotateMe = cloningMenu.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Button>();
        recycleMe = cloningMenu.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        deleteMe = cloningMenu.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Button>();

        rotateMe.onClick.AddListener(delegate { RotateMe(); });
        recycleMe.onClick.AddListener(delegate { RecycleMe(); });
        deleteMe.onClick.AddListener(delegate { DeleteMe(); });

        cloningMenu.SetActive(false);
        }

    public void SetFurnitureMenuActive()
        {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    public void SetFurnitureMenuUnActive()
        {
        Debug.Log("FurnitureMenu disabled.");
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    public void RotateMe()
        {
        transform.Rotate(Vector3.up, 90, Space.World);
        float eulerY = transform.eulerAngles.y;

        PlacedItemData placedItemData = GetComponent<PlacedItemData>();
        placedItemData.eulerAngle = eulerY;

        placedItemData.positionPlaced[0] = gameObject.transform.position.x;
        placedItemData.positionPlaced[1] = gameObject.transform.position.y;
        placedItemData.positionPlaced[2] = gameObject.transform.position.z;

        placedItemData.SaveItemPlaced();
        }

    public void RecycleMe()
        {
        Debug.Log("IF YOU HAVE A RECYCLER, YOU CAN RECYCLE " + gameObject.name);
        }

    public void DeleteMe()
        {
        furnitureMenu.SetActive(false);
        Debug.Log("REDI TO BE DELETED: MY INDEX " + myIndex);
        //PlacedItemsList placedItemsList = FindObjectOfType<PlacedItemsList>();
        //placedItemsList.DeleteItemFromList(myIndex);

        PlacedItemData placedItemData = GetComponent<PlacedItemData>();
        placedItemData.RemoveItemPlaced();
        Destroy(gameObject);
        }
    }