using System;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private EventChannelITowerCard triggerTowerPlacementChannel;

    private bool placementActivated = false;
    private ITowerCard currentTowerCard;
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

    private void TiggerTowerPlacement(ITowerCard triggerTowerCard)
    {
        if (currentTowerCard == triggerTowerCard)
        {
            ClearCurrentSelection();
            return;
        }
        if (triggerTowerCard.GetCost() > PlayerMoney.Instance.GetMoney())
        {
            Debug.Log("Cost is too big");
            if (placementActivated)
                ClearCurrentSelection();
            return;
        }

        currentTowerCard = triggerTowerCard;
        currentTowerCard.Select();
        placementActivated = true;

        currentTowerPreview = Instantiate(currentTowerCard.GetPreview(), transform);
        currentTowerPreview.Hide();
    }

    private void ClearCurrentSelection()
    {
        currentTowerCard?.Deselect();
        currentTowerCard = null;
        placementActivated = false;
        if (currentTowerPreview != null)
        {
            Destroy(currentTowerPreview.gameObject);
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
            if (TowerGrid.Instance.CanPlaceTower(mouseWorldPosition, out Vector2 cellPosition))
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
            if (PlayerMoney.Instance.GetMoney() < currentTowerCard.GetCost())
            {
                ClearCurrentSelection();
                return;
            }

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            if (TryPlaceTower(mouseWorldPosition))
            {
                PlayerMoney.Instance.TryDecreaseMoney(currentTowerCard.GetCost());
                ClearCurrentSelection();
            }
        }
    }

    private bool TryPlaceTower(Vector2 worldPosition)
    {
        return TowerGrid.Instance.TryPlaceTower(worldPosition, currentTowerCard.GetPrefab());
    }
}
