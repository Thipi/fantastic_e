using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SavingPlayer
{
    public static void SavePlayerDataToSystem(PlayerData playerData)
        {

        //Saving playerdata to system
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerData";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavePlayerData savePlayerData = new SavePlayerData(playerData);

        formatter.Serialize(stream, savePlayerData);
        stream.Close();
        }

    public static SavePlayerData LoadPlayerData( PlayerData playerData )
        {
        string path = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(path))
            {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavePlayerData savePlayerData = formatter.Deserialize(stream) as SavePlayerData;

            stream.Close();
            return savePlayerData;
            }
        else
            {
            throw new System.Exception("Given path is not available");
            }
        }
    public static void DeleteAllPlayerData()
        {
        if (File.Exists(Application.persistentDataPath + "/PlayerData"))
            {
            File.Delete(Application.persistentDataPath + "/PlayerData");
            }
        }
    }
