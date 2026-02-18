using UnityEngine;

public class SimpleProjectile : Projectile
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyMask;

    [Space]
    [SerializeField] private GameObject spriteObject;
    [SerializeField] private float angleOffset;

    Vector2 targetPosition;

    public override void Setup(Enemy target)
    {
        targetPosition = target.transform.position;
        RotateSpriteToMovement();
    }

    private void Update()
    {
        if (SessionStateManager.Instance.IsPaused)
            return;

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

    private void RotateSpriteToMovement()
    {
        if (spriteObject == null)
            return;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteObject.transform.rotation = Quaternion.Euler(0, 0, angle + angleOffset);
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

    private void TargetReached() => gameObject.SetActive(false);
    private void Dissolve() => gameObject.SetActive(false);
}
