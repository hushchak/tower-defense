using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    [SerializeField] private EventChannelTowerCardData triggerTowerPlacementChannel;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    private TowerCardData data;

    private void OnEnable() => button.onClick.AddListener(OnCardClicked);
    private void OnDisable() => button.onClick.RemoveListener(OnCardClicked);

    public void Setup(TowerCardData data)
    {
        this.data = data;

        cardName.text = data.Name;
        cardCost.text = data.Cost.ToString();
        image.sprite = data.Sprite;
    }

    private void OnCardClicked()
    {
        triggerTowerPlacementChannel.Raise(data);
    }

    public GameObject GetPrefab()
    {
        return data.Prefab;
    }
}
