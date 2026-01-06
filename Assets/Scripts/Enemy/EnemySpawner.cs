using UnityEngine;

public class EnemySpawner
{
    private Vector2 spawnPosition;
    private Path path;

    public EnemySpawner(Vector2 spawnPosition, Path path)
    {
        this.spawnPosition = spawnPosition;
        this.path = path;
    }

    public void SpawnEnemy(EnemyData enemyData)
    {
        Enemy enemy = Object.Instantiate(enemyData.Prefab, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
        enemy.Setup(path);
    }
}
