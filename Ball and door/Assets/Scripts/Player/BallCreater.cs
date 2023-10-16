using UnityEngine;

public class BallCreater : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    [SerializeField] private Transform posToCreateBall;

    [SerializeField] private float sizeIncreaseFactor;
    [SerializeField] private float speedIncreaseFactor;

    private float createBallSize;
    private float startBallSize;

    private GameObject createBall;

    [HideInInspector] public bool BallIsAlife;

    public void BallCreate()
    {
        BallIsAlife = true;
        createBall = Instantiate(ball, posToCreateBall.position, Quaternion.identity);
        createBallSize = startBallSize = createBall.transform.localScale.x;
    }

    public float BallSize()
    {
        createBallSize += (createBallSize * sizeIncreaseFactor) / PlayerBehaviour.SizeFactor;
        UpdateBallScale();
        return createBall.transform.localScale.x;
    }

    private void UpdateBallScale()
    {
        createBall.transform.localScale = new Vector3(createBallSize, createBallSize, createBallSize);
    }

    public void MoveBall()
    {
        if (createBall.GetComponent<MoveBall>() != null)
        {
            createBall.GetComponent<MoveBall>().MoveSettings(ConvertCoefficient(), this);
        }
        else
        {
            Debug.LogError("MoveBall component not found on the created ball.");
        }
    }

    private float ConvertCoefficient()
    {
        float temp = startBallSize / createBallSize;
        temp = temp / speedIncreaseFactor;
        return (createBallSize / startBallSize) * temp;
    }
}
