namespace Achievement
{
    public class AchievementList
    {
        public static readonly AchievementElement DONT_USE_SWORD = new AchievementElement(AchievementElement.MedalValues.GOLD_MEDAL, "Dövüşmeye Gerek Yok", "Öğretici dışında düşmanlara saldırmadan oyunu bitir.", false); // COMPLETED
        public static readonly AchievementElement DONT_DIE = new AchievementElement(AchievementElement.MedalValues.GOLD_MEDAL, "Seçilmiş Savaşçı", "Oyunu hiç ölmeden bitir.", false); // COMPLETED
        public static readonly AchievementElement PLATFORMER_BOSS = new AchievementElement(AchievementElement.MedalValues.BRONZE_MEDAL, "Platform Patronu", "Beşinci bölümdeki zorlu platformlardan geç.", false);
        public static readonly AchievementElement LAST_BANDITBENDER = new AchievementElement(AchievementElement.MedalValues.BRONZE_MEDAL, "Son Haydut Bükücü", "Beşinci bölümde düşmanlarla yüzleş.", false);
        public static readonly AchievementElement FIRST_BLOOD = new AchievementElement(AchievementElement.MedalValues.BRONZE_MEDAL, "Birçoğunun İlki", "Bir düşman öldür.", false); // COMPLETED
        public static readonly AchievementElement TRAINED = new AchievementElement(AchievementElement.MedalValues.BRONZE_MEDAL, "Eğitimli", "Eğitim bölümünü (ilk bölümü) bitir.", false); // COMPLETED
    }
}
