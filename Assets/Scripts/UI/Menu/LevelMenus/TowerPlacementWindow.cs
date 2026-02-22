using System;
using UnityEngine;

public class TowerPlacementWindow : Window, ILevelInitializable
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private MenuController controller;
    [Space]
    [SerializeField] private RectTransform cardParent;
    [SerializeField] private TowerCard cardPrefab;

    private TowerData[] towers;
    private TowerCard[] cards;

    private bool subscribed;
    private TowerPlacementPoint currentPlacementPoint;

    public override void Open()
    {
        Debug.LogError("Invalid open method");
    }

    public void Open(TowerPointClickData data)
    {
        SubscribeToCards();
        currentPlacementPoint = data.Point;

        menuObject.SetActive(true);
    }

    public override void Close()
    {
        if (subscribed)
            UnsubscribeToCards();
        currentPlacementPoint = null;

        menuObject.SetActive(false);
    }

    public void Initialize(LevelData data)
    {
        towers = data.Towers;
        cards = new TowerCard[towers.Length];

        InitializeCards(towers);
    }

    private void InitializeCards(TowerData[] towerData)
    {
        for (int i = 0; i < towerData.Length; i++)
        {
            TowerCard card = Instantiate(cardPrefab, cardParent.transform);
            card.Setup(towerData[i]);
            cards[i] = card;
        }
    }

    private void SubscribeToCards()
    {
        foreach (TowerCard card in cards)
        {
            card.OnClick += OnCardClick;
        }
        subscribed = true;
    }

    private void UnsubscribeToCards()
    {
        foreach (TowerCard card in cards)
        {
            card.OnClick -= OnCardClick;
        }
        subscribed = false;
    }

    private void OnCardClick(TowerData data)
    {
        if (PlayerMoney.Instance.TryDecreaseMoney(data.BuildCost))
        {
            UnsubscribeToCards();
            PlaceTower(data);
            controller.Close();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void PlaceTower(TowerData towerData)
    {
        currentPlacementPoint.TryPlaceTower(towerData);
    }
}
