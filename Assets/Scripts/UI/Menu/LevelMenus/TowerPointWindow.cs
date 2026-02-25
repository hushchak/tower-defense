using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointWindow : Window
{
    [Header("Events")]
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [Header("UI Objects")]
    [SerializeField] private GameObject menuObject;
    [SerializeField] private MenuController controller;
    [Space]
    [SerializeField] private Image towerPreview;
    [Header("Buttons")]
    [SerializeField] private Button sellButton;
    [SerializeField] private TMP_Text sellButtonText;
    [SerializeField] private string sellButtonPrompt = "Sell";
    [Space]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text upgradeButtonText;
    [SerializeField] private string upgradeButtonPrompt = "Upgrade";
    [Space]
    [SerializeField] private ToggleButton strategyButton;
    [SerializeField] private TowerTargetStrategy[] strategies;

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

        towerPreview.sprite = currentTower.GetPreviewImage();
        SetUpgradeButtonState();
        SetSellButtonState();
        SetStrategyButtonState();

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
        else
        {
            upgradeButtonText.text = upgradeButtonPrompt + ": " + currentTower.GetUpgradeCost().ToString();
            upgradeButton.interactable = currentPlacementPoint.EnoughMoneyForUpgrade();
        }
    }

    private void SetSellButtonState()
    {
        sellButtonText.text = sellButtonPrompt + ": " + currentTower.GetSellCost().ToString();
    }

    private void SetStrategyButtonState()
    {
        TowerTargetStrategy strategy = currentTower.GetTowerStrategy();
        for (int i = 0; i < strategies.Length; i++)
        {
            if (strategies[i] == strategy)
            {
                strategyButton.SetState(i);
            }
        }
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

    public void SetTowerStrategy(TowerTargetStrategy strategy)
    {
        currentPlacementPoint.SetTowerStrategy(strategy);
    }
}
