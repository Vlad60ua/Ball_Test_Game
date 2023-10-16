using System.Collections;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    [SerializeField] private float destroyToDelay = 0.5f;

    private AudioSource audioSource;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        particle.SetActive(false);
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ObstacleHit(Material material)
    {
        PlayObstacleSound();
        ChangeMaterial(material);
        StartCoroutine(DelayToDestroy());
    }

    private void PlayObstacleSound()
    {
        audioSource.PlayOneShot(SoundManager.instance.GetSound(false, false, true));
    }

    private void ChangeMaterial(Material material)
    {
        meshRenderer.material = material;
    }

    private IEnumerator DelayToDestroy()
    {
        yield return new WaitForSeconds(destroyToDelay);
        ShowParticleEffect();
        HideMeshRenderer();
        DestroyObstacle();
    }

    private void ShowParticleEffect()
    {
        particle.SetActive(true);
    }

    private void HideMeshRenderer()
    {
        meshRenderer.enabled = false;
    }

    private void DestroyObstacle()
    {
        Destroy(gameObject, destroyToDelay);
    }
}
