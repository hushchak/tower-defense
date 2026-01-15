using UnityEngine;

public class YSortStatic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * 1000) * -1;
    }
}
