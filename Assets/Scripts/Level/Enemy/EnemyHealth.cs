using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData data;

    public event Action Death;
    private int health;

    private void OnEnable()
    {
        health = data.HealthPoints;
    }

    public void ApplyDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, data.HealthPoints);
        Audio.Play(data.HurtSound);

        if (health == 0)
            Die();
    }

    private void Die()
    {
        Death?.Invoke();
    }
}
