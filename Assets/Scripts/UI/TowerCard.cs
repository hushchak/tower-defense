using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour
{
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardCost;
    [SerializeField] private Image image;

    private TowerCardData data;

    public void Setup(TowerCardData data)
    {
        this.data = data;

        cardName.text = data.Name;
        cardCost.text = data.Cost.ToString();
        image.sprite = data.Sprite;
    }

    public GameObject GetPrefab()
    {
        return data.Prefab;
    }
}
