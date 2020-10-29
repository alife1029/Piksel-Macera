using System.Collections.Generic;

namespace Achievement
{
    [System.Serializable]
    public class AchievementData
    {
        public List<AchievementElement> achievements;

        public bool bDontUseSword = false;
        public bool bDontDie = false;
        public bool bPlatformerBoss = false;
        public bool bLastBanditbender = false;
        public bool bFirstBlood = false;
        public bool bTrained = false;

        public AchievementData () {
            achievements = new List<AchievementElement>();

            // Achievements
            AchievementElement dontUseSword = AchievementList.DONT_USE_SWORD;
            dontUseSword.SetCompleted(bDontUseSword);
            AchievementElement dontDie = AchievementList.DONT_DIE;
            dontDie.SetCompleted(bDontDie);
            AchievementElement platformerBoss = AchievementList.PLATFORMER_BOSS;
            platformerBoss.SetCompleted(bPlatformerBoss);
            AchievementElement lastBanditbender = AchievementList.LAST_BANDITBENDER;
            lastBanditbender.SetCompleted(bLastBanditbender);
            AchievementElement firstBlood = AchievementList.FIRST_BLOOD;
            firstBlood.SetCompleted(bFirstBlood);
            AchievementElement trained = AchievementList.TRAINED;
            trained.SetCompleted(bTrained);

            // Adds achievements to list
            achievements.Add(dontUseSword);
            achievements.Add(dontDie);
            achievements.Add(platformerBoss);
            achievements.Add(lastBanditbender);
            achievements.Add(firstBlood);
            achievements.Add(trained);
        }
    }
}
