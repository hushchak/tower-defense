using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Data", fileName = "TowerData")]
public class TowerData : ScriptableObject
{
    [field: SerializeField] public TowerTargerStrategy TargetStrategy { get; private set; }
    [field: SerializeField] public LayerMask EnemyMask { get; private set; }

    [field: Header("Shooting")]
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public float Frequency { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public Sound ShotSound { get; private set; }
}
