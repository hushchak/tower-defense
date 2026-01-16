public class LevelData
{
    public readonly WaveData[] Waves;
    public readonly TowerCardData[] Towers;
    public readonly int PlayerMaxHealth;
    public readonly int PlayerStartMoney;

    public LevelData(WaveData[] waves, int playerMaxHealth, int playerStartMoney,
    TowerCardData[] towers)
    {
        Waves = waves;
        Towers = towers;
        PlayerMaxHealth = playerMaxHealth;
        PlayerStartMoney = playerStartMoney;
    }
}
