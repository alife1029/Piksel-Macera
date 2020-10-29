namespace Achievement
{
    [System.Serializable]
    public class AchievementElement
    {
        public enum MedalValues
        {
            BRONZE_MEDAL = 1,
            SILVER_MEDAL = 3,
            GOLD_MEDAL = 7
        }

        private MedalValues value;
        private string explanation;
        private string header;
        private bool completed;

        public AchievementElement (MedalValues value, string header, string explanation, bool completed) {
            this.value = value;
            this.header = header;
            this.explanation = explanation;
            this.completed = completed;
        }

        // Getter methods
        public MedalValues GetValue () { return value; }
        public string GetExplanation () { return explanation; }
        public string GetHeader () { return header; }
        public bool IsCompleted () { return completed; }
        public void SetCompleted (bool completed) { this.completed = completed; }
    }
}
