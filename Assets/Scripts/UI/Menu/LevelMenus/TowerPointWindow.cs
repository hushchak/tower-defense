using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointWindow : Window
{
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [Space]
    [SerializeField] private GameObject menuObject;
    [SerializeField] private MenuController controller;
    [Space]
    [SerializeField] private Button sellButton;
    [SerializeField] private TMP_Text sellButtonText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeButtonText;
    [Space]
    [SerializeField] private string sellButtonPrompt = "Sell";
    [SerializeField] private string upgradeButtonPrompt = "Upgrade";

    private TowerPlacementPoint currentPlacementPoint;
    private Tower currentTower;

    public override void Open()
    {
        Debug.LogError("Invalid open method");
    }

    public void Open(TowerPointClickData data)
    {
        currentPlacementPoint = data.Point;
        currentTower = data.Tower;

        SetUpgradeButtonState();
        SetSellButtonState();

        moneyChangedChannel.Subscribe(ValidateButtonState);
        menuObject.SetActive(true);
    }

    public override void Close()
    {
        menuObject.SetActive(false);
        currentPlacementPoint = null;
        moneyChangedChannel.Unsubscribe(ValidateButtonState);
    }

    private void ValidateButtonState(int money) => SetUpgradeButtonState();
    private void SetUpgradeButtonState()
    {
        if (!currentPlacementPoint.CanUpgradeTower())
        {
            upgradeButtonText.text = "Max level";
            upgradeButton.interactable = false;
        }
        else if (!currentPlacementPoint.EnoughMoneyForUpgrade())
        {
            upgradeButtonText.text = "Not enough money";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButtonText.text = upgradeButtonPrompt + ": " + currentTower.GetUpgradeCost().ToString();
            upgradeButton.interactable = true;
        }
    }

    private void SetSellButtonState()
    {
        sellButtonText.text = sellButtonPrompt + ": " + currentTower.GetSellCost().ToString();
    }

    public void Sell()
    {
        if (currentPlacementPoint.TrySellTower(out int cost))
        {
            PlayerMoney.Instance.AddMoney(cost);
            controller.Close();
        }
        else
        {
            Debug.LogError("Something went wrong. Tower in tower point is null");
        }
    }

    public void Upgrade()
    {
        if (currentPlacementPoint.TryUpgradeTower(out int cost))
        {
            PlayerMoney.Instance.DecreaseMoney(cost);
            SetUpgradeButtonState();
            SetSellButtonState();
        }
        else
        {
            Debug.LogError("Something went wrong when upgrading tower");
        }
    }
}
