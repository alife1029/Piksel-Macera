using UnityEngine;
using Achievement.UI;

namespace Achievement
{
    public class AchievementController
    {
        private GameObject popup;
        private Sprite bronzeMedal, silverMedal, goldenMedal;

        public static void AchievementCompleted (AchievementElement achievement, AchievementPopup popup) {
            AchievementData achievementData = AchievementSaveSystem.Read();
            if(achievement == AchievementList.DONT_USE_SWORD) achievementData.bDontUseSword = true;
            else if(achievement == AchievementList.DONT_DIE) achievementData.bDontDie = true;
            else if(achievement == AchievementList.PLATFORMER_BOSS) achievementData.bPlatformerBoss = true;
            else if(achievement == AchievementList.LAST_BANDITBENDER) achievementData.bLastBanditbender = true;
            else if(achievement == AchievementList.FIRST_BLOOD) achievementData.bFirstBlood = true;
            else if(achievement == AchievementList.TRAINED) achievementData.bTrained = true;
            else Debug.LogError("Achievement not found");

            AchievementSaveSystem.Save(achievementData);

            popup.ShowPopup(achievement.GetValue(), "Başarım Kazanıldı", achievement.GetHeader());
        }
    }
}
