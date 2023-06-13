using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad
{

    public static void SaveGame()
    {
        string path = Application.persistentDataPath + "tData.crr";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(path, FileMode.Create);
        GmaeSaveData TapData = new GmaeSaveData();
        formatter.Serialize(fs, TapData);
        fs.Close();
    }
    public static GmaeSaveData LoadGame()
    {
        string path = Application.persistentDataPath + "tData.crr";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            GmaeSaveData TapData = formatter.Deserialize(fs) as GmaeSaveData;
            fs.Close();
            return TapData;
        }
        else
        {
            return null;
        }
    }

}

