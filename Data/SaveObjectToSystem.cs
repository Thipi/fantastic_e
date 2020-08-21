using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveObjectToSystem
    {
    public static void SaveItem(StoreObjSerialized storeObject)
        {
        BinaryFormatter formatter = new BinaryFormatter();
        //Storing the item to persistent datapath under "crafteditems" with the name created and an int to avoid collisions.
        string path = Application.persistentDataPath + "/buildingsTESTFOLDER." + storeObject.nameOfObj;
        FileStream stream = new FileStream(path, FileMode.Create);


        SaveObjectData saveObjectData = new SaveObjectData(storeObject);

            formatter.Serialize(stream, storeObject);
            stream.Close();
        }


    public static SaveObjectData LoadObjectData(string pathname)
        {
        //TODO: Change the path to a valid one, once there is DATA.
        string path = Application.persistentDataPath + "/buildingsTESTFOLDER" + pathname;
        if (File.Exists(path))
            {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveObjectData data = formatter.Deserialize(stream) as SaveObjectData;
            stream.Close();

            return data;
            }
        else
            {
            Debug.Log("File not found in this location: " + path);
            return null;
            }

        }
    }
