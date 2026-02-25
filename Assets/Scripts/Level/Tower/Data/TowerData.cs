using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Data", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Information")]
    [field: SerializeField] public Tower Prefab { get; private set; }
    [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    [field: Space]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int BuildCost { get; private set; }

    [field: Header("Shooting")]
    [field: SerializeField] public TowerTargetStrategy DefaultTargetStrategy { get; private set; }
    [field: SerializeField] public LayerMask EnemyMask { get; private set; }

    [field: Header("Levels")]
    [field: SerializeField] public TowerLevelData[] LevelData { get; private set; }
}
