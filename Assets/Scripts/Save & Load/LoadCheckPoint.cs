using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadCheckPoint : MonoBehaviour {
    public GameObject[] checkPoints, objects;
    public Slider xpBar, healthBar;
    public Text xpLevel, xp, health;

    private void Start () {
        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if (playerData != null) {
            if(playerData.starting == "Load Checkpoint") {
                CheckpointData checkpointData = SaveSystem.Read(SaveSystem.checkpointDataPath) as CheckpointData;
                // Replace and load datas
                transform.position = checkPoints[checkpointData.checkpointIndex].transform.position;

                xpLevel.text = checkpointData.xpLevel.ToString();
                xpBar.maxValue = checkpointData.maxXp;
                xpBar.value = checkpointData.xp;
                xp.text = checkpointData.xp.ToString() + "/" + checkpointData.maxXp.ToString();

                healthBar.maxValue = checkpointData.maxHealth;
                healthBar.value = checkpointData.health;
                health.text = checkpointData.health.ToString() + " / " + checkpointData.maxHealth.ToString();

                // Destroy back objects
                for(int i = checkpointData.checkpointIndex; i >= 0; i--) {
                    Destroy(objects[i]);
                }

                // Disable back checkpoints
                for(int i = checkpointData.checkpointIndex; i >= 0; i--) {
                    checkPoints[i].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.tag == "Respawn") {
            CheckpointData checkpointData = new CheckpointData(
                collision.gameObject.GetComponent<CheckPoint>().id, 
                int.Parse(xpLevel.text), Convert.ToInt32(xpBar.maxValue),
                Convert.ToInt32(xpBar.value), Convert.ToInt32(healthBar.value),
                Convert.ToInt32(healthBar.maxValue)
            );

            SaveSystem.Save(checkpointData, SaveSystem.checkpointDataPath);
            GetComponent<PlayerStatus>().data.starting = "Load Checkpoint";
            GetComponent<PlayerStatus>().Save();

            Destroy(collision.gameObject);
        }
    }

    private void OnApplicationQuit () {
        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if(SceneManager.GetActiveScene().buildIndex > 1)
            playerData.starting = "Load Game";
        else
            playerData.starting = "New Game";

        SaveSystem.Save(playerData, SaveSystem.playerDataPath);
        SaveSystem.DestroyCheckpointFile();
    }
}
