[System.Serializable]
public class SettingsData
{
    public readonly int mainVolume, musicVolume, sfxVolume;
    public readonly float guiScale;

    public SettingsData(int mainVolume, int musicVolume, int sfxVolume, float guiScale) {
        this.mainVolume = mainVolume;
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
        this.guiScale = guiScale;
    }
}
