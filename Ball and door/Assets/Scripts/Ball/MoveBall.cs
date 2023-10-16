using System.Collections;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocity;

    private BallCreater ballCreater;

    private Coroutine moveCoroutine;

    private readonly string ObstacleTag = "Obstacle";
    private readonly string GateTag = "Gate";

    public void MoveSettings(float moveCoefficient, BallCreater ballCreater)
    {
        this.ballCreater = ballCreater;
        moveSpeed *= moveCoefficient;
        velocity = transform.forward * moveSpeed;
        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(ObstacleTag) || collision.transform.CompareTag(GateTag))
        {
            BallHit ballHit = gameObject.GetComponent<BallHit>();

            if (ballHit != null)
            {
                ballHit.Hit();
                ballCreater.BallIsAlife = false;
            }
            else
            {
                Debug.LogError("BallHit component not found on the ball: " + gameObject.name);
            }

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            transform.position += velocity * Time.deltaTime;

            yield return null;
        }
    }
}
