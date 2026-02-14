using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, ITowerCard
{
    [Header("Components")]
    [SerializeField] private EventChannelITowerCard triggerTowerPlacementChannel;
    [SerializeField] private Button button;
    [SerializeField] private Image background;

    [Header("Visuals")]
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;

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

    private void OnCardClicked() => triggerTowerPlacementChannel.Raise(this);

    public Tower GetPrefab() => data.Prefab;
    public TowerPreview GetPreview() => data.Preview;
    public int GetCost() => data.Cost;

    public void Select()
    {
        background.sprite = selectedSprite;
    }

    public void Deselect()
    {
        background.sprite = normalSprite;
    }
}
