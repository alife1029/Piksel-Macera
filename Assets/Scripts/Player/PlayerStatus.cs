using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public PlayerData data;

    private void Start () {
        data = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
    }

    public void Save () {
        SaveSystem.Save(data, SaveSystem.playerDataPath);
    }
}
