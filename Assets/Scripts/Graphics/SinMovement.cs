using UnityEngine;

public class SinMovement : MonoBehaviour
{
    [Header("X Axis")]
    public float xSpeed = 1f;
    public float xDistance = 0.25f;

    [Header("Y Axis")]
    public float ySpeed = 1f;
    public float yDistance = 0.25f;

    [Space]
    [SerializeField] private bool randomOffset = true;

    private float xOffset;
    private float yOffset;
    private Vector3 startPos;

    void Awake()
    {
        if (randomOffset)
        {
            xOffset = Random.value;
            yOffset = Random.value;
        }
        startPos = transform.localPosition;
    }

    void Update()
    {
        float x = Mathf.Sin((Time.time * xSpeed) + xOffset) * xDistance;
        float y = Mathf.Sin((Time.time * ySpeed) + yOffset) * yDistance;

        transform.localPosition = startPos + new Vector3(x, y, 0);
    }
}
