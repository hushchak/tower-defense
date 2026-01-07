using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;
    [SerializeField] private EventChannelInt damageEventChannel;

    private Path path;
    private int currentPoint;

    private int health;

    public void Setup(Path path)
    {
        this.path = path;
    }

    private void OnEnable()
    {
        currentPoint = 0;
        health = data.HealthPoints;
    }

    private void Update()
    {
        HandleMovement(Time.deltaTime);
    }

    private void HandleMovement(float delta)
    {
        if (currentPoint == path.GetPointsCount)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path.GetPoint(currentPoint).position,
            data.Speed * delta
        );

        if (Vector2.Distance(transform.position, path.GetPoint(currentPoint).position) < 0.1f)
        {
            currentPoint++;
            if (currentPoint >= path.GetPointsCount)
            {
                PathEndReached();
            }
        }
    }

    private void PathEndReached()
    {
        gameObject.SetActive(false);
        damageEventChannel.Raise(data.Damage);
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        health = health < 0
            ? 0
            : health;

        if (health == 0)
        {
            Death();
        }
    }
}
