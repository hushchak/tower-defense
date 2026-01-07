using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelInitializationEventChannel;

    [SerializeField] private EventChannelInt damageEventChannel;
    [SerializeField] private EventChannel dealthEventChannel;

    private int maxHealth;
    private int health;

    private void OnEnable()
    {
        levelInitializationEventChannel.Subsribe(Intialize);
        damageEventChannel.Subsribe(TakeDamage);
    }

    private void OnDisable()
    {
        levelInitializationEventChannel.Unsubscribe(Intialize);
        damageEventChannel.Unsubscribe(TakeDamage);
    }

    private void Intialize(LevelData data)
    {
        maxHealth = data.PlayerHealth;
        health = maxHealth;
    }

    private void TakeDamage(int damage)
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

    private void Death()
    {
        dealthEventChannel.Raise();
    }
}
