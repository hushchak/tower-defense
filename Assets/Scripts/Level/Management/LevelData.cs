public class LevelData
{
    public readonly int NextUnlockableLevelIndex;
    public readonly WaveData[] Waves;
    public readonly TowerCardData[] Towers;
    public readonly EnemySpawner EnemySpawner;
    public readonly int PlayerMaxHealth;
    public readonly int PlayerStartMoney;

    public LevelData(int nextUnlockableLevelIndex, WaveData[] waves, int playerMaxHealth, int playerStartMoney,
    TowerCardData[] towers, EnemySpawner enemySpawner)
    {
        NextUnlockableLevelIndex = nextUnlockableLevelIndex;
        Waves = waves;
        Towers = towers;
        PlayerMaxHealth = playerMaxHealth;
        PlayerStartMoney = playerStartMoney;
        EnemySpawner = enemySpawner;
    }
}
