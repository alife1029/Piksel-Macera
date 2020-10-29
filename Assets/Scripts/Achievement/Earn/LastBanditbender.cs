using UnityEngine;
using Achievement.UI;

namespace Achievement.Earn
{
    public class LastBanditbender : MonoBehaviour
    {
        public AchievementPopup achievementPopup;

        private void Start () {
            if(AchievementSaveSystem.Read().bLastBanditbender) {
                Destroy(this);
            }
        }

        private void OnTriggerEnter2D (Collider2D collision) {
            if(!AchievementSaveSystem.Read().bLastBanditbender) {
                AchievementController.AchievementCompleted(AchievementList.LAST_BANDITBENDER, achievementPopup);
                Debug.Log("New Achievement: Last Banditbender");
            }
        }
    }
}
