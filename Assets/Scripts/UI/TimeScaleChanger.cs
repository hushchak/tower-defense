using UnityEngine;

public class TimeScaleChanger : MonoBehaviour
{
    public void SetTimeScale(int scale) => SessionStateManager.Instance.ChangeTimeScale(scale);
}
