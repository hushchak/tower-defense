using UnityEngine;

[System.Serializable]
public class ProjectileData
{
    [field:SerializeField] public int Damage { get; private set;}
    [field:SerializeField] public float Speed { get; private set;}
    [field:SerializeField] public float Radius { get; private set;}
    [field:SerializeField] public LayerMask EnemyMask { get; private set;}
}
