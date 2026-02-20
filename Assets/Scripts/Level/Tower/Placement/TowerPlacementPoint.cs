using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacementPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private EventChannelTowerPointClickData onTowerPointClickChannel;

    private Tower tower;

    public void OnPointerClick(PointerEventData eventData)
    {
        onTowerPointClickChannel.Raise(new TowerPointClickData(this, tower));
    }

    public bool TryPlaceTower(TowerData data)
    {
        if (tower != null)
            return false;

        tower = Instantiate(data.Prefab, transform);
        return true;
    }

    public bool TrySellTower(out int cost)
    {
        if (tower == null)
        {
            cost = 0;
            return false;
        }

        cost = tower.GetCost();
        return true;
    }
}
