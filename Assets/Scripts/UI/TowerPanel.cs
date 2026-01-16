using System;
using UnityEngine;

public class TowerPanel : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private Transform cardParent;
    [SerializeField] private TowerCard cardPrefab;

    private TowerCardData[] Towers;

    public void Initialize(LevelData data)
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
