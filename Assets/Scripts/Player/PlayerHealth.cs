using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelInitializationEventChannel;

    [SerializeField] private EventChannelInt damageEventChannel;
    [SerializeField] private EventChannel dealthEventChannel;

    [SerializeField] private EventChannelInt healthChangedChannel;

    private int maxHealth;
    private int health;

    private void OnEnable()
    {
        levelInitializationEventChannel.Subscribe(Intialize);
        damageEventChannel.Subscribe(TakeDamage);
    }

    private void OnDisable()
    {
        levelInitializationEventChannel.Unsubscribe(Intialize);
        damageEventChannel.Unsubscribe(TakeDamage);
    }

    private void Intialize(LevelData data)
    {
        maxHealth = data.PlayerMaxHealth;
        health = maxHealth;
    }

    private void TakeDamage(int damage)
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
