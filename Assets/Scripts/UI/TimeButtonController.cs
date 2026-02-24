using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeButtonController : MonoBehaviour
{
    public enum TimeButtonType
    {
        None,
        x2,
        x3
    }

    [SerializeField] private TimeButton x2Button;
    [SerializeField] private TimeButton x3Button;
    [Space]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color hoverPressedColor;
    [Space]
    [SerializeField] private Sound pressButtonSound;
    [SerializeField] private Sound unpressButtonSound;

    private TimeButtonType currentTimeButton = TimeButtonType.None;

    private void OnEnable()
    {
        x2Button.OnClick += OnClick;
        x3Button.OnClick += OnClick;
        x2Button.OnEnter += OnHoverEnter;
        x3Button.OnEnter += OnHoverEnter;
        x2Button.OnExit += OnHoverExit;
        x3Button.OnExit += OnHoverExit;
    }

    private void OnDisable()
    {
        x2Button.OnClick -= OnClick;
        x3Button.OnClick -= OnClick;
        x2Button.OnEnter -= OnHoverEnter;
        x3Button.OnEnter -= OnHoverEnter;
        x2Button.OnExit -= OnHoverExit;
        x3Button.OnExit -= OnHoverExit;
    }

    private void OnClick(TimeButtonType buttonType)
    {
        if (buttonType == currentTimeButton)
        {
            currentTimeButton = TimeButtonType.None;
            Audio.Play(unpressButtonSound);
        }
        else
        {
            currentTimeButton = buttonType;
            Audio.Play(pressButtonSound);
        }
        SetTimeScale(currentTimeButton);
        UpdateButtonColors(x2Button);
        UpdateButtonColors(x3Button);
    }

    private void OnHoverEnter(TimeButtonType buttonType)
    {
        switch (buttonType)
        {
            case TimeButtonType.x2:
                x2Button.ChangeColorTo(currentTimeButton == TimeButtonType.x2 ? hoverPressedColor : hoverColor);
                break;
            case TimeButtonType.x3:
                x3Button.ChangeColorTo(currentTimeButton == TimeButtonType.x3 ? hoverPressedColor : hoverColor);
                break;
        }
    }

    private void OnHoverExit(TimeButtonType buttonType)
    {
        switch (buttonType)
        {
            case TimeButtonType.x2:
                x2Button.ChangeColorTo(currentTimeButton == TimeButtonType.x2 ? pressedColor : defaultColor);
                break;
            case TimeButtonType.x3:
                x3Button.ChangeColorTo(currentTimeButton == TimeButtonType.x3 ? pressedColor : defaultColor);
                break;
        }
    }

    private void UpdateButtonColors(TimeButton button)
    {
        button.ChangeColorTo(currentTimeButton == button.ButtonType ? pressedColor : defaultColor);
    }

    private void SetTimeScale(TimeButtonType buttonType)
    {
        switch (buttonType)
        {
            case TimeButtonType.None:
                SessionStateManager.Instance.ChangeTimeScale(1);
                break;
            case TimeButtonType.x2:
                SessionStateManager.Instance.ChangeTimeScale(2);
                break;
            case TimeButtonType.x3:
                SessionStateManager.Instance.ChangeTimeScale(3);
                break;
        }
    }
}
