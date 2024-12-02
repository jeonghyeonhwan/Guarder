[System.Serializable]
public class PlayerData
{
    public int level;
    public int experience;
    public int coins;
    public int diamonds;

    public PlayerData(int level, int experience, int coins, int diamonds)
    {
        this.level = level;
        this.experience = experience;
        this.coins = coins;
        this.diamonds = diamonds;
    }
}
