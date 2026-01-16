public class LevelData
{
    public readonly WaveData[] Waves;
    public readonly TowerCardData[] Towers;
    public readonly int PlayerMaxHealth;
    public readonly int PlayerStartMoney;

    public readonly TowerGrid TowerGrid;

    public LevelData(WaveData[] waves, int playerMaxHealth, int playerStartMoney, TowerGrid towerGrid,
    TowerCardData[] towers)
    {
        Waves = waves;
        Towers = towers;
        PlayerMaxHealth = playerMaxHealth;
        PlayerStartMoney = playerStartMoney;
        TowerGrid = towerGrid;
    }
}
