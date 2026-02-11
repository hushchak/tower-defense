using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private Sound sound;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
            Debug.Log($"{gameObject.name} does not have Button component");
    }

    private void OnEnable() => button.onClick.AddListener(PlaySound);
    private void OnDisable() => button.onClick.AddListener(PlaySound);

    private void PlaySound()
    {
        Audio.Play(sound);
    }
}
