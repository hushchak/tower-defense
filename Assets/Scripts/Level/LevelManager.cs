using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelIntializeEventChannel;
    [SerializeField] private EventChannel playerDeathEventChannel;

    [Space]
    [SerializeField] private LevelData levelData;

    private void OnEnable()
    {
        playerDeathEventChannel.Subsribe(Defeat);
    }

    private void OnDisable()
    {
        playerDeathEventChannel.Unsubscribe(Defeat);
    }

    private void Start()
    {
        levelIntializeEventChannel.Raise(levelData);
    }

    private void Defeat()
    {
        Debug.Log("Player defeated");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
