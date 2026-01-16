using System;
using UnityEngine;

public class TowerPanel : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelInitializationChannel;
    [SerializeField] private Transform cardParent;
    [SerializeField] private TowerCard cardPrefab;

    private TowerCardData[] Towers;

    private void OnEnable()
    {
        levelInitializationChannel.Subscribe(Setup);
    }

    private void OnDisable()
    {
        levelInitializationChannel.Unsubscribe(Setup);
    }

    private void Setup(LevelData data)
    {
        Towers = data.Towers;

        for (int i = 0; i < Towers.Length; i++)
        {
            AddTowerCard(Towers[i]);
        }
    }

    private void AddTowerCard(TowerCardData data)
    {
        TowerCard card = Instantiate(cardPrefab, cardParent);
        card.Setup(data);
    }
}
