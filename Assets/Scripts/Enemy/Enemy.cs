using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;

    private Path path;
    private int currentPoint;

    public void Setup(Path path)
    {
        this.path = path;
    }

    private void OnEnable()
    {
        currentPoint = 0;
    }

    private void Update()
    {
        HandleMovement(Time.deltaTime);
    }

    private void HandleMovement(float delta)
    {
        if (currentPoint == path.GetPointsCount)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path.GetPoint(currentPoint).position,
            data.Speed * delta
        );

        if (Vector2.Distance(transform.position, path.GetPoint(currentPoint).position) < 0.1f)
        {
            currentPoint++;
            if (currentPoint >= path.GetPointsCount)
            {
                PathEndReached();
            }
        }
    }

    private void PathEndReached()
    {
        gameObject.SetActive(false);
    }
}
