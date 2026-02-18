using UnityEngine;
using UnityEngine.Audio;

public static class Audio
{
    public enum Mixer
    {
        Master,
        SoundFX,
        Music
    }

    private static AudioMixer audioMixer;
    private static AudioSource audioSourcePrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        audioSourcePrefab = Resources.Load<AudioSource>("Audio/AudioSource");
        audioMixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
    }

    public static void Play(Sound sound)
    {
        AudioSource audioSource = Object.Instantiate(audioSourcePrefab);
        audioSource.clip = sound.Clip;
        audioSource.volume = sound.Volume;
        audioSource.pitch = sound.Pitch + Random.Range(0f, sound.RandomPitch);
        audioSource.Play();

        Object.Destroy(audioSource.gameObject, sound.Clip.length);
    }

    public static void SetVolume(Mixer mixer, float value)
    {
        switch (mixer)
        {
            case Mixer.Master:
                audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
                break;
            case Mixer.SoundFX:
                audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(value) * 20f);
                break;
            case Mixer.Music:
                audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
                break;
        }
    }
}
