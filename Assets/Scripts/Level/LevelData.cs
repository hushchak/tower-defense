public class LevelData
{
    public readonly WaveData[] Waves;
    public readonly int MaxPlayerHealth;

    public readonly TowerGrid TowerGrid;
    public readonly PlayerHealth PlayerHealth;

    public LevelData(WaveData[] waves, int maxPlayerHealth, TowerGrid towerGrid, PlayerHealth playerHealth)
    {
        Waves = waves;
        MaxPlayerHealth = maxPlayerHealth;
        TowerGrid = towerGrid;
        PlayerHealth = playerHealth;
    }
}
