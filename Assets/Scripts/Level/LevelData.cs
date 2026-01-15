public class LevelData
{
    public readonly WaveData[] Waves;
    public readonly int PlayerMaxHealth;
    public readonly int PlayerStartMoney;

    public readonly TowerGrid TowerGrid;

    public LevelData(WaveData[] waves, int playerMaxHealth, int playerStartMoney, TowerGrid towerGrid)
    {
        Waves = waves;
        PlayerMaxHealth = playerMaxHealth;
        PlayerStartMoney = playerStartMoney;
        TowerGrid = towerGrid;
    }
}
