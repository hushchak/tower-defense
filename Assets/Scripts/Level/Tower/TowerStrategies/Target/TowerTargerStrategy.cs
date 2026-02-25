using UnityEngine;

public abstract class TowerTargetStrategy : ScriptableObject
{
    public abstract Enemy GetTarget(Enemy[] enemies, Vector2 towerPosition, TowerData data);
}
