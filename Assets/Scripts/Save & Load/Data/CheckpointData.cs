[System.Serializable]
public class CheckpointData
{
    public int checkpointIndex, xpLevel, maxXp, xp, health, maxHealth;

    public CheckpointData (int checkpointIndex, int xpLevel, int maxXp, int xp, int health, int maxHealth) {        
        this.checkpointIndex = checkpointIndex;
        this.xpLevel = xpLevel;
        this.maxXp = maxXp;
        this.xp = xp;
        this.health = health;
        this.maxHealth = maxHealth;
    }
}
