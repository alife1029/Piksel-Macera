using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static readonly string playerDataPath = Application.persistentDataPath + @"\Player Save.alf";
    public static readonly string checkpointDataPath = Application.persistentDataPath + @"\Checkpoint Save.alf";
    public static readonly string settingsDataPath = Application.persistentDataPath + @"\Settings Save.alf";
    public static readonly string playerSnapshotDataPath = Application.persistentDataPath + @"\Player Snapshot.alf";

    public static void Save(object data, string path) {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static object Read (string path) {
        try {
            object data;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            data = formatter.Deserialize(stream);
            stream.Close();
            return data;
        } catch(System.Exception) {
            return null;
        }
    }

    public static void DestroyCheckpointFile() {
        if(File.Exists(checkpointDataPath))
            File.Delete(checkpointDataPath);
    }

    public static void DestroyAllFiles () {
        DestroyCheckpointFile();
        if(File.Exists(playerDataPath))
            File.Delete(playerDataPath);
        if(File.Exists(playerSnapshotDataPath))
            File.Delete(playerSnapshotDataPath);
    }

    public static void CreateAllFiles () {
        Save(new PlayerData(), playerDataPath);
        Save(new SettingsData(100, 100, 100, Application.isMobilePlatform ? 1.7f : 1.0f), settingsDataPath);
    }
}
