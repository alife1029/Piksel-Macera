using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel, settingsPanel, mainInterface;
    public AudioClip effect;
    public AudioSource backgroundMusic;

    void Start () {
        pauseMenuPanel.SetActive(false);
        mainInterface.SetActive(true);
    }

    void Update () {
        if(!Application.isMobilePlatform) {
            if((Input.GetKeyDown("joystick button 9") || Input.GetKeyDown(KeyCode.Escape)) || Input.GetKeyDown("joystick button 2")) {
                ChangeVisibility();
            }
        }
    }

    public void ChangeVisibility () {
        if(!settingsPanel.activeSelf) {
            GetComponent<AudioSource>().PlayOneShot(effect);
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
            mainInterface.SetActive(!mainInterface.activeSelf);

            if(pauseMenuPanel.activeSelf) {
                Time.timeScale = 0;
                backgroundMusic.mute = true;
            } else {
                Time.timeScale = 1;
                backgroundMusic.mute = false;
            }
        }
    }
}
