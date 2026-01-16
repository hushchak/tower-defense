using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>, ILevelInitializable
{
    [SerializeField] private EventChannel dealthEventChannel;
    [SerializeField] private EventChannelInt healthChangedChannel;

    private int maxHealth;
    private int health;

    public void Initialize(LevelData data)
    {
        maxHealth = data.PlayerMaxHealth;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health < 0
            ? 0
            : health;

        healthChangedChannel.Raise(health);

        if (health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        dealthEventChannel.Raise();
    }
}
