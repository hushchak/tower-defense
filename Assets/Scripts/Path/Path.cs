using UnityEditor;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private bool gizmos;

    public Transform GetPoint(int index) => points[index];
    public int GetPointsCount => points.Length;

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        if (points != null && points.Length > 0)
        {
            GUIStyle style = new();
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            for (int i = 0; i < points.Length; i++)
            {
                Handles.Label(points[i].transform.position + Vector3.up * 0.7f, points[i].name, style);
            }

            Gizmos.color = Color.gray;
            for (int i = 0; i < points.Length - 1; i++)
            {
                Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
            }
        }
    }
}
