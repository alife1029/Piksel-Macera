using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Achievement
{
    public class AchievementSaveSystem
    {
        public static readonly string achievementSavePath = Application.persistentDataPath + @"\Achievement Save.alf";

        public static void Save(AchievementData data) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(achievementSavePath, FileMode.Create);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        
        public static AchievementData Read () {
            if(!File.Exists(achievementSavePath)) {
                Save(new AchievementData());
                return Read();
            }

            try {
                AchievementData data;
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(achievementSavePath, FileMode.Open);
                data = formatter.Deserialize(stream) as AchievementData;
                stream.Close();
                return data;
            } catch(System.Exception) {
                return null;
            }
        }
    }
}
