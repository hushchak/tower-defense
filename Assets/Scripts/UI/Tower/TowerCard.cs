using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPointerClickHandler
{
    public event Action<TowerData> OnClick;

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Image towerPreviewImage;

    private TowerData data;

    public void Setup(TowerData data)
    {
        this.data = data;

        SetName(data.Name);
        SetCost(data.BuildCost);
        SetImage(data.Sprite);
    }

    private void SetName(string name) => nameText.text = name;
    private void SetCost(int cost) => costText.text = cost.ToString();
    private void SetImage(Sprite image) => towerPreviewImage.sprite = image;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(data);
    }
}
