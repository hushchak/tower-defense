using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public TimeButtonController.TimeButtonType ButtonType { get; private set; }
    [SerializeField] private Image image;

    public event Action<TimeButtonController.TimeButtonType> OnClick, OnEnter, OnExit;

    public void OnPointerClick(PointerEventData eventData) => OnClick?.Invoke(ButtonType);
    public void OnPointerEnter(PointerEventData eventData) => OnEnter?.Invoke(ButtonType);
    public void OnPointerExit(PointerEventData eventData) => OnExit?.Invoke(ButtonType);

    public void ChangeColorTo(Color color) => image.color = color;
}
