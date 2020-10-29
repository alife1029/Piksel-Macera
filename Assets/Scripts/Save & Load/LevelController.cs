using Achievement;
using Achievement.UI;

using System;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
    YAPILACAKLAR
    * BİR SONRAKİ LEVEL YERİNE UPGRADE LEVEL'İ YÜKLE
    * NORMAL KAYIT YERİNE SNAPSHOT KAYIT AL
    * NORMAL KAYDI UPGRADE İŞLEMİNDEN SONRA AL
    * 
    * NORMAL KAYIT ALIRKEN SNAPSHOT KAYIT ÜZERİNDEN KAYDI AL 
*/
public class LevelController : MonoBehaviour {
    public Slider xpBar, healthBar;
    public Text xpLevel, xpBarText, healthBarText;
    public AudioClip nextLevelAudio;
    public GameObject loadPanel;
    public GameObject mainGui;
    public PlayerStatus playerStatus;
    public Slider progressBar;
    public Text progressBarText;
    private bool active = true;

    public AchievementPopup achievementPopup;

    AudioSource audioSource;

    // Load the game
    private void Start () {
        audioSource = GetComponent<AudioSource>();

        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if (playerData != null) {
            if(playerData.starting == "Load Game") {
                // Xp Data
                xpLevel.text = playerData.startingLevel.ToString();
                xpBar.maxValue = playerData.startingMaxXp;
                xpBar.value = playerData.startingXp;

                // Health data
                if(playerData.sceneIndex != 1) {
                    healthBar.maxValue = 25;
                    healthBar.value = 25;
                } else {
                    healthBar.maxValue = 25;
                    healthBar.value = 12;
                }

            }

            xpBarText.text = playerData.startingXp.ToString() + "/" + playerData.startingMaxXp.ToString();
            healthBarText.text = healthBar.value + "/" + healthBar.maxValue;
        }
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Player" && active) {
            audioSource.PlayOneShot(nextLevelAudio);
            AchievementData achievementData = AchievementSaveSystem.Read();
            if (SceneManager.GetActiveScene().buildIndex == 1 && !achievementData.bTrained) {
                AchievementController.AchievementCompleted(AchievementList.TRAINED, achievementPopup);
                Invoke("LoadNextLevel", 4.2f);
            } else Invoke("LoadNextLevel", 0.8f);

            active = false;
        }
    }

    private void LoadNextLevel () {
        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        playerData.starting = SceneManager.GetActiveScene().buildIndex == 1 ? "New Game" : "Load Game";
        SaveSystem.Save(playerData, SaveSystem.playerDataPath);

        playerStatus.data.sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        playerStatus.data.xp = playerStatus.data.startingXp = Convert.ToInt32(xpBar.value);
        playerStatus.data.maxXp = playerStatus.data.startingMaxXp = Convert.ToInt32(xpBar.maxValue);
        playerStatus.data.xpLevel = playerStatus.data.startingLevel = Convert.ToInt32(xpLevel.text);
        playerStatus.data.starting = "Load Game";
        
        progressBar.value = 3;
        progressBarText.text = "%" + (Mathf.RoundToInt((progressBar.value / progressBar.maxValue) * 100)).ToString();

        // Saving the game as a snapshot
        SaveSystem.Save(playerStatus.data, SaveSystem.playerSnapshotDataPath);
        progressBar.value = 6;
        progressBarText.text = "%" + (Mathf.RoundToInt((progressBar.value / progressBar.maxValue) * 100)).ToString();

        // Reset checkpoint
        SaveSystem.DestroyCheckpointFile();
        progressBar.value = 9;
        progressBarText.text = "%" + (Mathf.RoundToInt((progressBar.value / progressBar.maxValue) * 100)).ToString();

        mainGui.SetActive(false);
        loadPanel.SetActive(true);
        StartCoroutine(StartLoading());
    }
    IEnumerator StartLoading () {
        AsyncOperation async = SceneManager.LoadSceneAsync("UpgradeScene");

        while(!async.isDone) {
            progressBar.value = 9 + async.progress;
            progressBarText.text = "%" + (Mathf.Round((progressBar.value / progressBar.maxValue) * 100)).ToString();
            yield return null;
        }
    }
}
