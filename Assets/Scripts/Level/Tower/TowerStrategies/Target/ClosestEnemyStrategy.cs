using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Strategies/Target/Closest Enemy", fileName = "ClosestEnemyStrategy")]
public class ClosestEnemyStrategy : TowerTargerStrategy
{
    public override Enemy GetTarget(Enemy[] enemies, Vector2 towerPosition, TowerData data)
    {
        if (enemies.Length == 0)
            return null;

        float distance = float.MaxValue;
        int index = -1;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector2.Distance(towerPosition, enemies[i].transform.position) < distance)
            {
                distance = Vector2.Distance(towerPosition, enemies[i].transform.position);
                index = i;
            }
        }

        return enemies[index];
    }
}
