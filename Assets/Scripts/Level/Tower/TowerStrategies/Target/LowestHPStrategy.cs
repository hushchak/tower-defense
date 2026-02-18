using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Strategies/Target/HP Lowest", fileName = "HPLowestStrategy")]
public class LowestHPStrategy : TowerTargerStrategy
{
    public override Enemy GetTarget(Enemy[] enemies, Vector2 towerPosition, TowerData data)
    {
        if (enemies.Length == 0)
            return null;

        int health = int.MaxValue;
        int index = -1;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetHealth() < health)
            {
                health = enemies[i].GetHealth();
                index = i;
            }
        }

        return enemies[index];
    }
}
