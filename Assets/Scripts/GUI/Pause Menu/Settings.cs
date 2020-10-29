using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    public Slider mainVolume, musicVolume, sfxVolume, guiScale;
    public GameObject pauseMenuPanel;

    public GUIScaler mainInterfaceScaler;
    public GUIScaler pauseMenuScaler;
    
    void Start () {
        SettingsData settingsData = SaveSystem.Read(SaveSystem.settingsDataPath) as SettingsData;

        // Audio Settings
        mainVolume.value = settingsData.mainVolume;
        musicVolume.value = settingsData.musicVolume;
        sfxVolume.value = settingsData.sfxVolume;
        guiScale.value = settingsData.guiScale;
    }

    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 2")) {
            SaveAndExit();
        }
    }

    public void SaveAndExit () {
        // Save the settings
        SaveSystem.Save(new SettingsData(Convert.ToInt32(mainVolume.value),
                                        Convert.ToInt32(musicVolume.value),
                                        Convert.ToInt32(sfxVolume.value),
                                        guiScale.value),
                                        SaveSystem.settingsDataPath);

        // Apply the settings
        pauseMenuScaler.Start();
        mainInterfaceScaler.Start();

        // Change visibility
        pauseMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
