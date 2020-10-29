using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public AudioClip clickAudio;
    public Button continueButton;
    public Slider loadProgressBar;
    public Text progressBarText;
    public GameObject panel;
    public Animator levelAnimator;
    public Canvas loadingCanvas;

    private void Load(int level) {
        StartCoroutine(StartLoading(level));
    }
    IEnumerator StartLoading(int level) {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone) {
            loadProgressBar.value = 9 + async.progress * 10;
            progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
            yield return null;
        }
    }

    private void Load (string level) {
        StartCoroutine(StartLoading(level));
    }
    IEnumerator StartLoading (string level) {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while(!async.isDone) {
            loadProgressBar.value = 9 + async.progress * 10;
            progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
            yield return null;
        }
    }

    private void Start () {
        if(!System.IO.File.Exists(SaveSystem.playerDataPath)) {
            SaveSystem.Save(new PlayerData(), SaveSystem.playerDataPath);
            continueButton.enabled = false;
            continueButton.GetComponent<Image>().color = new Color(0.7075472f, 0.540686f, 0.01001244f);
        }
        if(!System.IO.File.Exists(SaveSystem.settingsDataPath)) {
            SaveSystem.Save(new SettingsData(100, 100, 100, Application.isMobilePlatform ? 1.7f : 1.0f),
                            SaveSystem.settingsDataPath);
            continueButton.enabled = false;
            continueButton.GetComponent<Image>().color = new Color(0.7075472f, 0.540686f, 0.01001244f);
        }

        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if(playerData.starting == "New Game") {
            continueButton.enabled = false;
            continueButton.GetComponent<Image>().color = new Color(0.7075472f, 0.540686f, 0.01001244f);
        }
    }

    public void btnNewGame_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        panel.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        SaveSystem.DestroyAllFiles();
        loadProgressBar.value = 9 / 2;
        progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
        SaveSystem.CreateAllFiles();
        loadProgressBar.value = 9;
        progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
        Load(1);
    }

    public void btnLoadGame_Click () {
        panel.SetActive(false);
        loadingCanvas.gameObject.SetActive(true);
        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        loadProgressBar.value = 9 / 2;
        progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        SaveSystem.DestroyCheckpointFile();
        loadProgressBar.value = 9;
        progressBarText.text = (loadProgressBar.value / loadProgressBar.maxValue).ToString("P");
        playerData.starting = "Load Game";
        try {
            Load(playerData.sceneIndex);   
        } catch (System.Exception) {
            SaveSystem.DestroyAllFiles();
            Load(1);
        }
    }

    public void btnEndlessPlatforms_Click() {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        SaveSystem.DestroyCheckpointFile();
        Load("EndlessPlatforms");
    }

    public void btnAchievements_Click() {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        Load("AchievementsScene");
    }

    public void btnSettings_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        Load("SettingsScene");
    }

    public void btnQuit_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        SaveSystem.DestroyCheckpointFile();
        Application.Quit();
    }
}
