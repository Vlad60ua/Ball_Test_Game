using UnityEngine;

public class SphereHit : MonoBehaviour
{
    [SerializeField] private Material material;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<ObstacleBehaviour>().ObstacleHit(material);
        }
    }
}
