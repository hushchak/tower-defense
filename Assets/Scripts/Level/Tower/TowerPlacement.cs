using System;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private EventChannelTowerCardData triggerTowerPlacementChannel;

    private bool placementActivated = false;
    private TowerCardData currentTower;

    private void OnEnable()
    {
        triggerTowerPlacementChannel.Subscribe(TiggerTowerPlacement);
        InputReader.OnMouseClick += OnMouseClick;
    }

    private void OnDisable()
    {
        triggerTowerPlacementChannel.Unsubscribe(TiggerTowerPlacement);
        InputReader.OnMouseClick -= OnMouseClick;
    }

    private void TiggerTowerPlacement(TowerCardData data)
    {
        if (currentTower == data)
        {
            ClearCurrentSelection();
            return;
        }
        if (data.Cost > PlayerMoney.Instance.GetMoney())
        {
            Debug.Log("Cost is too big");
            if (placementActivated)
                ClearCurrentSelection();
            return;
        }

        currentTower = data;
        placementActivated = true;
    }

    private void ClearCurrentSelection()
    {
        currentTower = null;
        placementActivated = false;
    }

    private void Update()
    {
        if (!placementActivated)
            return;

        UpdateTowerPreview();
    }

    private void UpdateTowerPreview()
    {
        Debug.Log(currentTower.Name);
    }

    private void OnMouseClick(Vector2 mouseScreenPosition)
    {
        if (placementActivated && PlayerMoney.Instance.TryDecreaseMoney(currentTower.Cost))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            TowerGrid.Instance.TryPlaceTower(mouseWorldPosition, currentTower.Prefab);
        }
        ClearCurrentSelection();
    }
}
