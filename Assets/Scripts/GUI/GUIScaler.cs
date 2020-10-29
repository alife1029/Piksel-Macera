using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GUIScaler : MonoBehaviour
{
    public void Start()
    {
        if (File.Exists(SaveSystem.settingsDataPath))
        GetComponent<CanvasScaler>().scaleFactor = (SaveSystem.Read(SaveSystem.settingsDataPath) as SettingsData).guiScale;
    }
}
