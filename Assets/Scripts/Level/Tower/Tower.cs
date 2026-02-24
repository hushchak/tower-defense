using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData data;
    [SerializeField] private Transform poolTransform;

    private TowerModel model;

    private float waitTime = 0;
    private GameObjectPool projectilePool;

    public int GetSellCost() => model.SellCost;
    public int GetUpgradeCost() => model.GetUpgradeCost();
    public bool CanUpgrade() => model.CanUpgrade();
    public bool EnoughMoneyForUpgrade() => model.EnoughMoneyForUpgrade();
    public bool TryUpgrade(out int cost) => model.TryUpgrade(out cost);

    private void Awake()
    {
        model = new TowerModel(data);
    }

    private void Update()
    {
        if (SessionStateManager.Instance.IsPaused)
            return;

        if (waitTime <= 0)
        {
            Shoot();
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

#region Shooting
    private void Shoot()
    {
        Enemy target = model.CurrentTowerStrategy.GetTarget(
            GetEnemiesInRadius(model.Radius, data.EnemyMask),
            transform.position,
            data
        );
        if (target != null)
        {
            SpawnProjectile(target);
            waitTime = model.Frequency;
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
        projectile.Setup(model.ProjectileData, target);

        projectile.gameObject.SetActive(true);
        Audio.Play(model.ShotSound);
    }

    private GameObject GetProjectile()
    {
        if (projectilePool == null)
            projectilePool = new GameObjectPool(data.ProjectilePrefab.gameObject, poolTransform, 1);
        return projectilePool.GetObject();
    }
#endregion

    private void OnDrawGizmos()
    {
        if (data == null)
            return;

        Gizmos.color = Color.blue;
        if (data.LevelData != null)
            Gizmos.DrawWireSphere(transform.position, model == null ? data.LevelData[0].Radius : model.Radius);
    }
}
