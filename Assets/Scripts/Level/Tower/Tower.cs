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
            Enemy target = GetClosestEnemy(data.Radius, data.EnemyMask);
            if (target != null)
            {
                SpawnProjectile(target);
                waitTime = data.Frequency;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void SpawnProjectile(Enemy target)
    {
        Projectile projectile = GetProjectile().GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Setup(target);

        projectile.gameObject.SetActive(true);
    }

    private GameObject GetProjectile()
    {
        if (projectilePool == null)
            projectilePool = new GameObjectPool(data.ProjectilePrefab, poolTransform, 1);
        return projectilePool.GetObject();
    }

    private Enemy GetClosestEnemy(float radius, LayerMask mask)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            radius,
            mask
        );

        if (targets.Length == 0)
            return null;

        float distance = float.MaxValue;
        int index = -1;
        for (int i = 0; i < targets.Length; i++)
        {
            if (Vector2.Distance(transform.position, targets[i].transform.position) < distance)
            {
                distance = Vector2.Distance(transform.position, targets[i].transform.position);
                index = i;
            }
        }

        if (targets[index].gameObject.TryGetComponent(out Enemy enemy))
        {
            return enemy;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (data == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, data.Radius);
    }
}
