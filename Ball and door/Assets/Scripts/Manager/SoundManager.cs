using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip ballHitSound;
    [SerializeField] private AudioClip blowSound;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverSound(bool isWin)
    {
        AudioClip soundToPlay = isWin ? winSound : loseSound;
        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }

    public AudioClip GetSound(bool jump = false, bool ballHit = false, bool blow = false)
    {
        if (jump)
        {
            return jumpSound;
        }
        else if (ballHit)
        {
            return ballHitSound;
        }
        else if (blow)
        {
            return blowSound;
        }

        return null;
    }
}
