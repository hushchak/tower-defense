using UnityEngine;

public class SimpleProjectile : Projectile
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyMask;

    Vector2 targetPosition;

    public override void Setup(Enemy target)
    {
        targetPosition = target.transform.position;
    }

    private void Update()
    {
        MoveToTarget(Time.deltaTime);
        CheckEnemies();
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            Dissolve();
        }
    }

    private void MoveToTarget(float delta)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * delta);
    }

    private void CheckEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, radius, enemyMask);

        if (enemy != null)
        {
            if (enemy.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(damage);
                TargetReached();
            }
        }
    }

    private void TargetReached()
    {
        gameObject.SetActive(false);
    }

    private void Dissolve()
    {
        gameObject.SetActive(false);
    }
}
