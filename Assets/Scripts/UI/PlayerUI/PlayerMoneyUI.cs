using TMPro;
using UnityEngine;

public class PlayerMoneyUI : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private EventChannelInt moneyChangedChannel;

    private void OnEnable()
    {
        moneyChangedChannel.Subscribe(UpdateText);
    }

    private void OnDisable()
    {
        moneyChangedChannel.Unsubscribe(UpdateText);
    }

    public void Initialize(LevelData data)
    {
        UpdateText(data.PlayerStartMoney);
    }

    private void UpdateText(int money)
    {
        moneyText.text = money.ToString();
    }
}
