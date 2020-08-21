using Assets.Analytics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;

public class ResourcesHandlerInScene : MonoBehaviour
{
    [SerializeField]
    GameObject[] resSprites = new GameObject[3];
    Image[] resImg = new Image[3];

    Text[] name, amount;

    GameObject[] resArray;

    [HideInInspector]
    public string stashType;

    int listLenght;

    [SerializeField]
    Text neutronCoinsAmount;

    [SerializeField]
    AudioSource coinbling;
    public void SelectingResources(string stashtype)
        {
        switch (stashtype)
            {
            case "common":
                break;
            case "uncommon":
                break;
            case "rare":
                listLenght = 3;
                break;
            default:
                break;
            }

        Debug.Log("List lenght: " + listLenght);
        GameObject[] listingAllResources = Resources.LoadAll<GameObject>("ResourcesAll");
        Sprite[] resourceSprites = Resources.LoadAll<Sprite>("ResourceSprites");
        resArray = new GameObject[listLenght];
        resImg = new Image[listLenght];
        name = new Text[listLenght];
        amount = new Text[listLenght];

        for (int i = 0; i < listLenght; i++)
            {

            int selected = Random.Range(0, listingAllResources.Length);

            Debug.Log("Selection number = " + selected);

            resArray[i] = selectedItem( listingAllResources[selected] );
            resImg[i] = resSprites[i].GetComponent<Image>();
            resImg[i].sprite = selectedImage( resourceSprites[selected] );

            Debug.Log("Selected Resource: " + resImg[i].name);
            name[i] = resSprites[i].transform.GetChild(0).transform.gameObject.GetComponent<Text>();
            amount[i] = resSprites[i].transform.GetChild(0).GetChild(0).transform.gameObject.GetComponent<Text>();

            name[i].text = resArray[i].name;
            amount[i].text = Random.Range(1, 4).ToString();
            }
        AddToInventory(resArray);
        StartCoroutine(AddNeutronCoins());
        }

    IEnumerator AddNeutronCoins()
        {
        //TODO: Change the hardcoded value accordingly when the stashtypes are implemented
        for (int i = 0; i < Random.Range(50, 100); i++)
            {
            coinbling.Play();
            neutronCoinsAmount.text = i.ToString();
            yield return new WaitForSeconds(0.02f);
            }
        }

    void AddToInventory( GameObject[] resource )
        {
        //HERE WE PUT THEM TO JSON ETC. ETC. Check from the MapCoordinates how this actually goes.
        int index = 0;
        foreach (GameObject item in resArray)
            {
                if(item.name == resource[index].name)
                {
                CollectingRes colRes = resource[index].GetComponent<CollectingRes>();
                AnalyticsClient.SendAnalytics("Resource added to Players inventory: " + item.name);
                colRes.CollectThisItem();
                }
            index++;
            }
        }

    GameObject selectedItem(GameObject selected)
        {
            return selected;
        }

    Sprite selectedImage( Sprite selected )
        {
        return selected;
        }
}
