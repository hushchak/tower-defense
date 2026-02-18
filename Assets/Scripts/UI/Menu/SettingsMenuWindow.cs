using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuWindow : Window
{
    [SerializeField] private GameObject menuObject;

    [Header("Audio Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundFXSlider;
    [SerializeField] private Slider musicSlider;

    public override void Open()
    {
        SetSliders();
        menuObject.SetActive(true);
    }

    public override void Close()
    {
        menuObject.SetActive(false);
    }

    private void SetSliders()
    {
        PreferencesData preferences = SaveManager.GetPreferencesData();
        masterSlider.value = preferences.MasterVolume;
        soundFXSlider.value = preferences.SoundFXVolume;
        musicSlider.value = preferences.MusicVolume;
    }

    public void SetMasterVolume(float level) => Audio.SetVolume(Audio.Mixer.Master, level);
    public void SetSoundFXVolume(float level) => Audio.SetVolume(Audio.Mixer.SoundFX, level);
    public void SetMusicVolume(float level) => Audio.SetVolume(Audio.Mixer.Music, level);
}
