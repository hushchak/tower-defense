using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData data;
    [SerializeField] private Transform poolTransform;

    private float waitTime = 0;
    private GameObjectPool projectilePool;

    private void Update()
    {
        if (waitTime <= 0)
        {
            Shoot();
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Enemy target = data.TargetStrategy.GetTarget(
            GetEnemiesInRadius(data.Radius, data.EnemyMask),
            transform.position,
            data
        );
        if (target != null)
        {
            SpawnProjectile(target);
            waitTime = data.Frequency;
        }
    }

    private Enemy[] GetEnemiesInRadius(float radius, LayerMask enemyLayer)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        List<Enemy> enemyList = new();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemyList.Add(enemy);
            }
        }
        return enemyList.ToArray();
    }

    private void SpawnProjectile(Enemy target)
    {
        Projectile projectile = GetProjectile().GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Setup(target);

        projectile.gameObject.SetActive(true);
        Audio.Play(data.ShotSound);
    }

    private GameObject GetProjectile()
    {
        if (projectilePool == null)
            projectilePool = new GameObjectPool(data.ProjectilePrefab, poolTransform, 1);
        return projectilePool.GetObject();
    }

    private void OnDrawGizmos()
    {
        if (data == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.Radius);
    }
}
