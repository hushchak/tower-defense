using UnityEngine;

public class SimpleProjectile : Projectile
{
    Vector2 targetPosition;

    public override void Setup(ProjectileData data, Enemy target)
    {
        base.Setup(data, target);
        targetPosition = target.transform.position;
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Data.Speed * delta);
    }


    private void CheckEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, Data.Radius, Data.EnemyMask);

        if (enemy != null)
        {
            if (enemy.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(Data.Damage);
                TargetReached();
            }
        }
    }

    private void TargetReached() => gameObject.SetActive(false);
    private void Dissolve() => gameObject.SetActive(false);
}
