using System;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private EventChannelTowerCardData triggerTowerPlacementChannel;

    private bool placementActivated = false;
    private TowerCardData currentTower;
    private TowerPreview currentTowerPreview;

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
        currentTowerPreview = Instantiate(currentTower.Preview, transform);
        currentTowerPreview.Hide();
    }

    private void ClearCurrentSelection()
    {
        currentTower = null;
        placementActivated = false;
        if (currentTowerPreview != null)
        {
            Destroy(currentTowerPreview);
            currentTowerPreview = null;
        }
    }

    private void Update()
    {
        if (!placementActivated)
            return;

        UpdateTowerPreview();
    }

    private void UpdateTowerPreview()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputReader.MousePosition);
        if (TowerGrid.Instance.PositionInsideGrid(mouseWorldPosition))
        {
            currentTowerPreview.Show();
            if(TowerGrid.Instance.CanPlaceTower(mouseWorldPosition, out Vector2 cellPosition))
            {
                currentTowerPreview.UnplaceableColor(false);
            }
            else
            {
                currentTowerPreview.UnplaceableColor(true);
            }
            currentTowerPreview.transform.position = cellPosition;
        }
        else
        {
            currentTowerPreview.Hide();
        }
    }

    private void OnMouseClick(Vector2 mouseScreenPosition)
    {
        if (placementActivated)
        {
            if (PlayerMoney.Instance.GetMoney() < currentTower.Cost)
            {
                ClearCurrentSelection();
                return;
            }

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            if (TryPlaceTower(mouseWorldPosition))
            {
                PlayerMoney.Instance.TryDecreaseMoney(currentTower.Cost);
                ClearCurrentSelection();
            }
        }
    }

    private bool TryPlaceTower(Vector2 worldPosition)
    {
        return TowerGrid.Instance.TryPlaceTower(worldPosition, currentTower.Prefab);
    }
}
