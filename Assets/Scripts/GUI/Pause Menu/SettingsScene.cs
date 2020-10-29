using UnityEngine.SceneManagement;

public class SettingsScene : Settings
{
    public void GoMainMenu () {
        SaveSystem.Save(new SettingsData((int)base.mainVolume.value, (int)base.musicVolume.value, 
                        (int)base.sfxVolume.value, base.guiScale.value),
                        SaveSystem.settingsDataPath);
        SceneManager.LoadScene("MainMenu");
    }
}
