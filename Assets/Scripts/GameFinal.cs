using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Achievement;
using Achievement.UI;

public class GameFinal : MonoBehaviour
{
    public Text buttonText;
    public AchievementPopup achievementPopup;

    void Start () {
        buttonText.text = Application.isMobilePlatform ? "TEBRİKLER OYUNU BİTİRDİNİZ!\nANA MENÜYE DÖNMEK İÇİN\nYILDIZA DOKUNUN"
            : "TEBRİKLER OYUNU BİTİRDİNİZ!\nANA MENÜYE DÖNMEK İÇİN\nYILDIZA TIKLAYIN";

        PlayerData playerData = SaveSystem.Read(SaveSystem.playerDataPath) as PlayerData;
        AchievementData achievementData = AchievementSaveSystem.Read();
        if (playerData.deathCount == 0 && !achievementData.bDontDie) {
            AchievementController.AchievementCompleted(AchievementList.DONT_DIE, achievementPopup);
            Debug.Log("New Achievement : Dont Die");
        }
        if (playerData.swordUseCount == 0 && !achievementData.bDontUseSword) {
            AchievementController.AchievementCompleted(AchievementList.DONT_USE_SWORD, achievementPopup);
            Debug.Log("New Achievement : Dont Use Sword");
        }

        // Deletes all save files
        SaveSystem.DestroyAllFiles();
    }

    public void goMainMenu() {
        // Loads main menu
        SceneManager.LoadScene(0);
    }
}
