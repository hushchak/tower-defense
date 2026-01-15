using TMPro;
using UnityEngine;

public class PlayerMoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [SerializeField] private EventChannelLevelData levelInitializeChannel;

    private void OnEnable()
    {
        levelInitializeChannel.Subscribe(Setup);
        moneyChangedChannel.Subscribe(UpdateText);
    }

    private void OnDisable()
    {
        levelInitializeChannel.Unsubscribe(Setup);
        moneyChangedChannel.Unsubscribe(UpdateText);
    }

    private void Setup(LevelData data)
    {
        UpdateText(data.PlayerStartMoney);
    }

    private void UpdateText(int money)
    {
        moneyText.text = money.ToString();
    }
}
