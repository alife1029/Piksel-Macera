using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUpgrade : MonoBehaviour
{
    PlayerData playerSnapshot, playerSnapshotOnStart, mainPlayerData;
    public Text maxHealthText, attackPowerText, defencePowerText, movementSpeedText, xpMultiplierText, usableAbilityPointText;

    public Slider progressBar;
    public Text progressBarText;
    public GameObject mainGui, loadPanel;

    int usableAbilityPoint;

    void Start()
    {
        playerSnapshot = SaveSystem.Read(SaveSystem.playerSnapshotDataPath) as PlayerData;
        playerSnapshotOnStart = SaveSystem.Read(SaveSystem.playerSnapshotDataPath) as PlayerData;
        mainPlayerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;

        usableAbilityPoint = playerSnapshotOnStart.xpLevel - mainPlayerData.xpLevel;

        maxHealthText.text = playerSnapshot.maxHealth.ToString();
        attackPowerText.text = playerSnapshot.attackPower.ToString();
        defencePowerText.text = playerSnapshot.defencePower.ToString("F1");
        movementSpeedText.text = playerSnapshot.maxSpeed.ToString("F1");
        xpMultiplierText.text = playerSnapshot.xpMultiplier.ToString("F1");
        usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + usableAbilityPoint.ToString();
    }

    // Increase button click events
    public void IncreaseMaxHealth_Click () {
        if (usableAbilityPoint > 0) {
            playerSnapshot.maxHealth += 8;
            maxHealthText.text = playerSnapshot.maxHealth.ToString();
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (--usableAbilityPoint).ToString();
        }
    }
    public void IncreaseAttackPower_Click () {
        if(usableAbilityPoint > 0) {
            playerSnapshot.attackPower += 1;
            attackPowerText.text = playerSnapshot.attackPower.ToString();
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (--usableAbilityPoint).ToString();
        }
    }
    public void IncreaseDefence_Click () {
        if(usableAbilityPoint > 0) {
            playerSnapshot.defencePower += 0.3f;
            defencePowerText.text = playerSnapshot.defencePower.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (--usableAbilityPoint).ToString();
        }
    }
    public void IncreaseMovementSpeed_Click () {
        if(usableAbilityPoint > 0) {
            playerSnapshot.maxSpeed += 0.4f;
            movementSpeedText.text = playerSnapshot.maxSpeed.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (--usableAbilityPoint).ToString();
        }
    }
    public void IncreaseXpMultiplier_Click () {
        if(usableAbilityPoint > 0) {
            playerSnapshot.xpMultiplier += 0.2f;
            xpMultiplierText.text = playerSnapshot.xpMultiplier.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (--usableAbilityPoint).ToString();
        }
    }

    // Decrease button click events
    public void DecreaseMaxHealth_Click () {
        if (playerSnapshot.maxHealth - 8 >= playerSnapshotOnStart.maxHealth) {
            playerSnapshot.maxHealth -= 8;
            maxHealthText.text = playerSnapshot.maxHealth.ToString();
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (++usableAbilityPoint).ToString();
        }
    }
    public void DecreaseAttackPower_Click () {
        if(playerSnapshot.attackPower - 1 >= playerSnapshotOnStart.attackPower) {
            playerSnapshot.attackPower -= 1;
            attackPowerText.text = playerSnapshot.attackPower.ToString();
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (++usableAbilityPoint).ToString();
        }
    }
    public void DecreaseDefence_Click () {
        if(playerSnapshot.defencePower - 0.3f <= playerSnapshotOnStart.defencePower) {
            playerSnapshot.defencePower -= 0.3f;
            defencePowerText.text = playerSnapshot.defencePower.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (++usableAbilityPoint).ToString();
        }
    }
    public void DecreaseMovementSpeed_Click () {
        if(playerSnapshot.maxSpeed - 0.4f >= playerSnapshotOnStart.maxSpeed) {
            playerSnapshot.maxSpeed -= 0.4f;
            movementSpeedText.text = playerSnapshot.maxSpeed.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (++usableAbilityPoint).ToString();
        }
    }
    public void DecreaseXpMultiplier_Click () {
        if(playerSnapshot.xpMultiplier - 0.2f >= playerSnapshotOnStart.xpMultiplier) {
            playerSnapshot.xpMultiplier -= 0.2f;
            xpMultiplierText.text = playerSnapshot.xpMultiplier.ToString("F1");
            usableAbilityPointText.text = "Kullanılabilir Yetenek Puanı: " + (++usableAbilityPoint).ToString();
        }
    }

    // Upgrade button click event
    public void BtnUpgrade_Click () {
        if (usableAbilityPoint == 0) {
            LoadNextLevel();
        }
    }

    // Level load system
    private void LoadNextLevel () {
        // Saving the game as a player save and delete snapshot
        SaveSystem.Save(playerSnapshot, SaveSystem.playerDataPath);
        progressBar.value = 4.5f;
        progressBarText.text = (progressBar.value / progressBar.maxValue).ToString("P");
        if(File.Exists(SaveSystem.playerSnapshotDataPath))
            File.Delete(SaveSystem.playerSnapshotDataPath);

        // Reset checkpoint
        SaveSystem.DestroyCheckpointFile();
        progressBar.value = 9;
        progressBarText.text = (progressBar.value / progressBar.maxValue).ToString("P");

        mainGui.SetActive(false);
        loadPanel.SetActive(true);
        StartCoroutine(StartLoading());
    }
    IEnumerator StartLoading () {
        AsyncOperation async = SceneManager.LoadSceneAsync(playerSnapshot.sceneIndex);

        while(!async.isDone) {
            progressBar.value = 9 + async.progress * 10;
            progressBarText.text = (progressBar.value / progressBar.maxValue).ToString("P");
            yield return null;
        }
    }
}
