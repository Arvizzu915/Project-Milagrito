using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundObject : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sounds;
    private AudioSource audioSource;

    private void Awake()
    {
        if (sounds.Length == 0)
            Debug.LogWarning("Faltan sonidos");
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    public void PlaySoundIndex(int index, float volume)
    {
        if (index >= sounds.Length || audioSource.isPlaying) return;
        audioSource.volume = volume;
        audioSource.PlayOneShot(sounds[index]);
    }

    public void CancelCurrentSound()
    {
        audioSource.Stop();
    }
}
