using UnityEngine;

public class WayBehaviour : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private Transform door;

    private float initialWaySize;

    private void Start()
    {
        initialWaySize = transform.localScale.x;
    }

    private void Update()
    {
        float distance = Vector3.Distance(ball.position, door.position);

        // Use a conditional expression to set GameObject active
        gameObject.SetActive(distance >= 2f);

        float newWaySize = Mathf.Max(initialWaySize, distance / 10);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newWaySize);

        float zPosition = ball.position.z + (distance / 2) - 0.5f;
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    public void WaySize(float playerBallSize)
    {
        transform.localScale = new Vector3(playerBallSize * initialWaySize, transform.localScale.y, transform.localScale.z);
    }
}
