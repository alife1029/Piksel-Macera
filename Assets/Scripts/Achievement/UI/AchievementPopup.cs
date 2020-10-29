using UnityEngine;
using UnityEngine.UI;

namespace Achievement.UI
{
    public class AchievementPopup : MonoBehaviour
    {
        public GameObject popup;
        public Sprite bronzeMedal, silverMedal, goldenMedal;

        public void ShowPopup(AchievementElement.MedalValues achievementValue, 
                                     string messageHdr, string achievementHeader) {
            GameObject createdPopup = Instantiate(popup).transform.GetChild(0).transform.GetChild(0).gameObject;

            // Achievement image
            createdPopup.transform.GetChild(0).GetComponent<Image>()
                .sprite = achievementValue == AchievementElement.MedalValues.BRONZE_MEDAL ? bronzeMedal
                        : achievementValue == AchievementElement.MedalValues.SILVER_MEDAL ? silverMedal
                        : bronzeMedal;
            // Message header text
            createdPopup.transform.GetChild(1).GetComponent<Text>().text = messageHdr;
            // Achievement header text
            createdPopup.transform.GetChild(2).GetComponent<Text>().text = achievementHeader;
        }
    }
}
