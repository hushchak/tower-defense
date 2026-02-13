using System;
using UnityEngine;

public class TowerSelector : MonoBehaviour, ILevelInitializable
{
    public event Action TowerPlaced, TowerCantBePlaced, TowerCardSelected, TowerCardSelectionDenied;

    [Header("Events")]
    [SerializeField] private EventChannelITowerCard triggerTowerPlacementChannel;

    private bool placementActivated = false;
    private TowerPlacement placement;
    private ITowerCard currentTowerCard;

    public void Initialize(LevelData _)
    {
        placement = new TowerPlacement(gameObject.transform);
    }

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

    private void OnMouseClick(Vector2 mouseScreenPosition)
    {
        if (!placementActivated)
            return;

        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        if (placement.TryPlaceTowerAt(currentTowerCard.GetPrefab(), mouseWorldPosition))
        {
            PlayerMoney.Instance.DecreaseMoney(currentTowerCard.GetCost());
            ClearCurrentSelection();
            TowerPlaced?.Invoke();
        }
        else
        {
            ClearCurrentSelection();
            TowerCantBePlaced?.Invoke();
        }
    }

    private void TiggerTowerPlacement(ITowerCard triggerTowerCard)
    {
        if (!CanSelectCard(triggerTowerCard))
        {
            TowerCardSelectionDenied?.Invoke();
            if (placementActivated)
                ClearCurrentSelection();
            return;
        }

        currentTowerCard = triggerTowerCard;
        currentTowerCard.Select();
        TowerCardSelected?.Invoke();

        placementActivated = true;
        placement.CreateTowerPreview(currentTowerCard.GetPreview());
    }

    private bool CanSelectCard(ITowerCard towerCard)
    {
        return (towerCard != currentTowerCard)
        && (towerCard.GetCost() <= PlayerMoney.Instance.GetMoney());
    }

    private void Update()
    {
        if (!placementActivated)
            return;

        placement.UpdateTowerPreview(Camera.main.ScreenToWorldPoint(InputReader.MousePosition));
    }

    private void ClearCurrentSelection()
    {
        currentTowerCard?.Deselect();
        currentTowerCard = null;
        placementActivated = false;
        placement.DestroyTowerPreview();
    }
}
