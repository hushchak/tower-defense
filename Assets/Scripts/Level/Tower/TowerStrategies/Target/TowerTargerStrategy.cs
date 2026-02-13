using UnityEngine;

public abstract class TowerTargerStrategy : ScriptableObject
{
    public abstract Enemy GetTarget(Enemy[] enemies, Vector2 towerPosition, TowerData data);
}
