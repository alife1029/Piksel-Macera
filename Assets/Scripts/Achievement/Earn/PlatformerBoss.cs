using UnityEngine;
using Achievement.UI;

namespace Achievement.Earn
{
    public class PlatformerBoss : MonoBehaviour
    {
        public AchievementPopup achievementPopup;

        private void OnTriggerEnter2D (Collider2D collision) {
            if(!AchievementSaveSystem.Read().bPlatformerBoss) {
                AchievementController.AchievementCompleted(AchievementList.PLATFORMER_BOSS, achievementPopup);
                Debug.Log("New Achievement: Platformer Boss");
            }
        }
    }
}
