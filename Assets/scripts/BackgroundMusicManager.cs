using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip chineseMusic;
    private SoundObject myAudioSource;

    private void Start()
    {
        if (TryGetComponent(out myAudioSource))
        {
            myAudioSource.PlaySound(chineseMusic);
        }
    }
}
