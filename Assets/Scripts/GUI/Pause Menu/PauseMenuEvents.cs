using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuEvents : MonoBehaviour {
    public AudioClip clickAudio;
    public GameObject mainPauseManu, settingsMenu, mainInterface;

    public void btnContinue_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        GetComponent<PauseMenu>().backgroundMusic.mute = false;
        mainPauseManu.SetActive(false);
        mainInterface.SetActive(true);
        Time.timeScale = 1;
    }

    public void btnRestart_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);

        SaveSystem.DestroyCheckpointFile();
        PlayerData data = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if(SceneManager.GetActiveScene().buildIndex > 1)
            data.starting = "Load Game";
        else
            data.starting = "New Game";
        SaveSystem.Save(data, SaveSystem.playerDataPath);

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void btnSettings_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        settingsMenu.SetActive(true);
        mainPauseManu.SetActive(false);
    }

    public void btnMainMenu_Click () {
        GetComponent<AudioSource>().PlayOneShot(clickAudio);
        
        PlayerData data = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        if(SceneManager.GetActiveScene().buildIndex > 1)
            data.starting = "Load Game";
        else
            data.starting = "New Game";

        SaveSystem.Save(data, SaveSystem.playerDataPath);
        SaveSystem.DestroyCheckpointFile();

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
