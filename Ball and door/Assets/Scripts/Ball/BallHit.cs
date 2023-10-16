using UnityEngine;

public class BallHit : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject obstacleDetector;
    [SerializeField] private GameObject particle;

    [SerializeField] private float timeToDestroy;

    private AudioSource audioSource;

    private void Start()
    {
        particle.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        particle.SetActive(true);

        if (audioSource != null)
        {
            AudioClip sound = SoundManager.instance.GetSound(false, true);
            if (sound != null)
            {
                audioSource.PlayOneShot(sound);
            }
        }

        ball.SetActive(false);
        obstacleDetector.SetActive(true);
        Destroy(gameObject, timeToDestroy);
    }
}
