[System.Serializable]
public class PlayerData
{
    public int sceneIndex, xp, maxXp, xpLevel, killCount, deathCount, swordUseCount;
    public int startingXp, startingMaxXp, startingLevel;
    public string starting;

    public int maxHealth, attackPower;
    public double maxSpeed, xpMultiplier, defencePower;

    public PlayerData () 
    {
        sceneIndex = 1;
        xp = 0;
        maxXp = 10;
        xpLevel = 1;
        starting = "New Game";
        killCount = 0;
        deathCount = 0;
        swordUseCount = 0;
        maxHealth = 25;
        attackPower = 5;
        defencePower = 1.0;
        maxSpeed = 8.0;
        xpMultiplier = 1.0;

        startingLevel = xpLevel;
        startingMaxXp = maxXp;
        startingXp = xp;
    }

    public PlayerData ( int sceneIndex, int xp, int maxXp, int xpLevel, string starting, 
                        int killCount, int deathCount, int swordUseCount,
                        int maxHealth, int attackPower, double defencePower,
                        double maxSpeed, double xpMultiplier) 
    {
        this.sceneIndex = sceneIndex;
        this.xp = xp;
        this.maxXp = maxXp;
        this.xpLevel = xpLevel;
        this.starting = starting;
        this.killCount = killCount;
        this.deathCount = deathCount;
        this.swordUseCount = swordUseCount;
        this.maxHealth = maxHealth;
        this.attackPower = attackPower;
        this.defencePower = defencePower;
        this.maxSpeed = maxSpeed;
        this.xpMultiplier = xpMultiplier;

        startingLevel = xpLevel;
        startingMaxXp = maxXp;
        startingXp = xp;
    }
}
