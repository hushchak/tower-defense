using Unity.VisualScripting;
using UnityEngine;

public class TowerPreview : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject spriteObject;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color unplaceableColor = Color.red;

    public void Show()
    {
        spriteObject.SetActive(true);
    }

    public void Hide()
    {
        spriteObject.SetActive(false);
    }

    public void SetUnplaceableColor(bool color)
    {
        spriteRenderer.color = color ? unplaceableColor : normalColor;
    }
}
