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

        cost = tower.GetSellCost();
        Destroy(tower.gameObject);
        tower = null;
        return true;
    }

    public bool CanUpgradeTower() => tower != null ? tower.CanUpgrade() : false;
    public bool EnoughMoneyForUpgrade() => tower != null ? tower.EnoughMoneyForUpgrade() : false;
    public bool TryUpgradeTower(out int cost) => tower.TryUpgrade(out cost);

    public void SetTowerStrategy(TowerTargetStrategy strategy) => tower.SetTowerStrategy(strategy);
}
