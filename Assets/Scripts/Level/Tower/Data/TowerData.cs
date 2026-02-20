using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Data", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Information")]
    [field: SerializeField] public Tower Prefab { get; private set; }
    [field: Space]
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }

    [field: Header("Meta shooting")]
    [field: SerializeField] public TowerTargerStrategy TargetStrategy { get; private set; }
    [field: SerializeField] public LayerMask EnemyMask { get; private set; }

    [field: Header("Shooting")]
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public float Frequency { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public Sound ShotSound { get; private set; }
}
