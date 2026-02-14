using UnityEngine;

public class ScribbleEffect : MonoBehaviour
{
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float positionScatter = 0.05f;
    [SerializeField] private float rotationScatter = 5f;

    private float timer = 0f;

    private void Update()
    {
        if (timer > frequency)
        {
            transform.localPosition = GetRandomPosition();
            transform.localRotation = GetRandomRotation();
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * positionScatter;
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(-rotationScatter, rotationScatter));
    }
}
