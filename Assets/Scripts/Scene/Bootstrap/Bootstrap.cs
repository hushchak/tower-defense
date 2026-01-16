using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private async void Start()
    {
        await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu);
    }
}
