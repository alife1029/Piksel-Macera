using UnityEngine;

namespace Achievement.UI
{
    public class AchievementUIManager : MonoBehaviour
    {
        public GameObject achievementElement;
        public Sprite goldenStar, silverStar, bronzeStar;

        private void Start () {
            AchievementData data = AchievementSaveSystem.Read();

            GetComponent<RectTransform>().sizeDelta = new Vector2(0, data.achievements.Count * 216 + 136);
            
            for (int i = 0; i < data.achievements.Count; i++) {
                GameObject newElement = Instantiate(achievementElement, gameObject.transform);
                AchievementUIElement elements = newElement.GetComponent<AchievementUIElement>();

                elements.header.text = data.achievements[i].GetHeader();
                elements.explanation.text = data.achievements[i].GetExplanation();
                elements.image.sprite =   data.achievements[i].GetValue() == AchievementElement.MedalValues.GOLD_MEDAL ? goldenStar
                                        : data.achievements[i].GetValue() == AchievementElement.MedalValues.SILVER_MEDAL ? silverStar
                                        : bronzeStar;
            }
        }
    }
}
