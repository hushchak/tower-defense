using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public int HealthPoints { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int MoneyValue { get; private set; }
    [field: Space]
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
