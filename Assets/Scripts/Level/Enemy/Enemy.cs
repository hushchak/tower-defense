using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Deactivated;

    [SerializeField] private EnemyData data;
    [SerializeField] private EnemyHealth healthSystem;

    private Path path;
    private int currentPoint;

    public int GetHealth() => healthSystem.GetHealth();

    public void Setup(Path path)
    {
        this.path = path;
    }

    private void OnEnable()
    {
        healthSystem.Death += Die;
        currentPoint = 0;
    }

    private void OnDisable()
    {
        healthSystem.Death -= Die;
    }

    private void Update()
    {
        if (SessionStateManager.Instance.IsPaused)
            return;

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
            transform.position = path.GetPoint(currentPoint).position;
            currentPoint++;
            if (currentPoint >= path.GetPointsCount)
            {
                PathEndReached();
            }
        }
    }

    private void PathEndReached()
    {
        Deactivated?.Invoke(this);
        gameObject.SetActive(false);
        PlayerHealth.Instance.TakeDamage(data.Damage);
    }

    private void Die()
    {
        Deactivated?.Invoke(this);
        gameObject.SetActive(false);
        PlayerMoney.Instance.AddMoney(data.MoneyValue);
    }
}
