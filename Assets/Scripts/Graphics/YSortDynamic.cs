using UnityEngine;

public class YSortDynamic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * 1000) * -1;
    }
}
