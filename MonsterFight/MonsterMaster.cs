using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class MonsterMaster : MonoBehaviour
{
    /* Commented out. This needs to be done with clips, not sources.
    public AudioSource normal;
    public AudioSource common;
    public AudioSource rare;
    public AudioSource legendary;
    */
    public GameObject[] monsters;
    public GameObject[] beatenMonsters = new GameObject[9];
    private int chosenMonster;
    private int wonMonster;
    private Scene scene;
    private MFUI mfUimaster; //For UI elements in MonsterFight scene.
    private Monster monsteriskripta;
    //public static MonsterWon[] monstersWon;

    [HideInInspector]
    public GameObject chosenMonsterForFight; //The object obtained from PlayerPrefs

    // Start is called before the first frame update
    private void Awake()
    {
        //monstersWon = LoadWonMonsterState();

        mfUimaster = FindObjectOfType<MFUI>();
        chosenMonster = PlayerPrefs.GetInt("monster");
        scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 2)
        {
            int index = 0;

            foreach (GameObject monster in monsters)
            {
                if (index == chosenMonster)
                {
                    monsteriskripta = monster.GetComponent<Monster>();
                    mfUimaster.monsterImage.sprite = monsteriskripta.thisMonsterImage;
                    chosenMonsterForFight = monster;
                    break;
                }
                index++;
            }
        }
        /* TODO This needs to be fixed/finished for Monsters to spawn to Base.
        if(scene.buildIndex == 3)
        {
            int index = 0;
            foreach (GameObject monster in beatenMonsters)
            {
                if (monstersWon[index].won == true)
                {
                    SpawnTheMonsterToBase(monster, index);
                }
                index++;
            }
        }*/
    }

    /* TODO This function needs to be Finished in order to get the monster to spawn to base!
    void SpawnTheMonsterToBase(GameObject monsterObj, int pivotNumber)
    {
        Transform homePivot = GameObject.Find("pivot" + pivotNumber).transform;
        GameObject monsterGuy = Instantiate(monsterObj, homePivot.position, homePivot.transform.rotation);
        monsterGuy.transform.SetParent(pivot);
    }*/

    public void ChooseMonster(GameObject monster)
    {
        MonsterIndexNUmber mindex = monster.GetComponent<MonsterIndexNUmber>();
        chosenMonster = mindex.myIndexNumber;
        PlayerPrefs.SetInt("monster", chosenMonster);
    }

/*
    public void AddBeatenMonster(GameObject monster)
    {
        MonsterIndexNUmber mindex = monster.GetComponent<MonsterIndexNUmber>();
        wonMonster = mindex.myIndexNumber;

        int index = 0;
        foreach (GameObject montrosity in beatenMonsters)
        {
            if (index == wonMonster)
            {
                if (beatenMonsters[index] == null)
                {
                    montrosity.SetActive(true);
                    PlayerPrefs.SetInt("BeatenMonster" + index, wonMonster);
                }
                break;
            }
            index++;
        }
    }

    // adds won monster to monstersWon array THIS NEED TO BE USED BY MONSTERFIGHT WITH INDEX NUMBER
    // OF DEFEATED MONSTER!!!
    public void AddWonMonster(int index)
    {
        MonsterWon[] wonMonList = LoadWonMonsterState();

        wonMonList[index].won = true;

        SaveWonMonsterState(wonMonList);
    }

    ///<summary>
    ///<para>Creates and initializes the won monsters json file with a resource obj array, if the file doesn't exist yet</para>
    ///</summary>
    public static void InitWonMonstersJson()
    {
        string resourceSaveFilePath = Application.persistentDataPath + "/WonMonsters.json";
        if (!File.Exists(resourceSaveFilePath))
        {
            MonsterWon[] wonMonList = new MonsterWon[monstersWon.Length];
            for (int i = 0; i < monstersWon.Length; i++)
            {
                wonMonList[i] = new MonsterWon() { won = false };
            }

            using (FileStream fs = File.Create(resourceSaveFilePath))
            {
                // create empty file
            }

            SaveWonMonsterState(wonMonList);
        }
    }

    ///<summary>
    ///<para>Loads the wonMonster json file and returns a resourceObj array with correct resource states</para>
    ///</summary>
    public static MonsterWon[] LoadWonMonsterState()
    {
        string resourceSaveFilePath = Application.persistentDataPath + "/WonMonsters.json";
        if (!File.Exists(resourceSaveFilePath))
        {
            InitWonMonstersJson();
        }

        // read the json file to a string
        string json = "";
        using (StreamReader sr = new StreamReader(resourceSaveFilePath))
        {
            json = sr.ReadToEnd();
        }

        // create a ResourceObj array and set correct resource states to it
        MonsterWon[] wonMonList;
        wonMonList = JsonHelper.FromJson<MonsterWon>(json);

        return wonMonList;
    }

    ///<summary>
    ///<para>Takes a monstersWon array and saves the resource data in it to the json file</para>
    ///</summary>
    public static void SaveWonMonsterState(MonsterWon[] wonMonsters)
    {
        string resourceSaveFilePath = Application.persistentDataPath + "/WonMonsters.json";
        if (!File.Exists(resourceSaveFilePath))
        {
            InitWonMonstersJson();
        }

        // convert resource obj array to JSON
        string json = JsonHelper.ToJson(wonMonsters, true);

        // save resource data to json file
        using (StreamWriter sw = new StreamWriter(resourceSaveFilePath, false))
        {
            sw.Write(json);
        }
    }

    [Serializable]
    public class MonsterWon
    {
        public bool won;
    }

    // to serialize/deserialize a *object array* to json copied from: (https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity)
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }*/
}
