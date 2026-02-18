using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private async void Start()
    {
        await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu);

        PreferencesData preferences = SaveManager.GetPreferencesData();
        Audio.SetVolume(Audio.Mixer.Master, preferences.MasterVolume);
        Audio.SetVolume(Audio.Mixer.SoundFX, preferences.SoundFXVolume);
        Audio.SetVolume(Audio.Mixer.Music, preferences.MusicVolume);
    }

    private void OnApplicationQuit()
    {
        EventChannel applicationQuitChannel = Resources.Load<EventChannel>("Events/ApplicationQuitChannel");
        applicationQuitChannel.Raise();
    }
}
