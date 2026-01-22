using UnityEngine;

public static class Audio
{
    private static AudioSource audioSourcePrefab;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        audioSourcePrefab = Resources.Load<AudioSource>("Audio/AudioSource");
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
}
