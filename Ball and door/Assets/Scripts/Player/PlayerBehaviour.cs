using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private WayBehaviour wayBehaviour;
    [SerializeField] private BallCreater createBall;
    [SerializeField] private CharacterPlyerController characterController;

    [SerializeField] private float timeToInputDelay = 0.1f;

    [SerializeField] private float sizeDeflatingCoefficient = 1f;

    private LoseWinState loseWinState;

    private float startPlayerBallSize;
    private float playerBallSize;

    private Coroutine sizeTimerCoroutine;

    private bool buttonDown;
    private bool canShot;

    public static float SizeFactor = 50f;

    private void Start()
    {
        loseWinState = LoseWinState.instance;
        playerBallSize = gameObject.transform.localScale.x;
        startPlayerBallSize = playerBallSize;
    }

    private void Update()
    {
        if (!loseWinState.stopGame)
        {
            canShot = !createBall.BallIsAlife && !characterController.Jump;

            if (createBall.BallIsAlife)
            {
                characterController.enabled = false;
            }
            else
            {
                characterController.enabled = true;
            }

            HandleInput();
        }
    }

    private void HandleInput()
    {
        // Check for mouse input on the Unity Editor
#if UNITY_EDITOR
        HandleMouseInput();
#else
        // Check for touch input on other platforms
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && !buttonDown && canShot)
        {
            CreateAndStartSizing();
        }
        else if (Input.GetMouseButtonUp(0) && buttonDown)
        {
            MoveBall();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !buttonDown && canShot)
            {
                CreateAndStartSizing();
            }
            else if (touch.phase == TouchPhase.Ended && buttonDown)
            {
                MoveBall();
            }
        }
    }

    private void CreateAndStartSizing()
    {
        createBall.BallCreate();
        sizeTimerCoroutine = StartCoroutine(SizeTimer());
        buttonDown = true;
    }

    private void MoveBall()
    {
        StopCoroutine(sizeTimerCoroutine);
        createBall.MoveBall();
        buttonDown = false;
    }

    private IEnumerator SizeTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToInputDelay);
            playerBallSize -= startPlayerBallSize / (SizeFactor / sizeDeflatingCoefficient);

            if (playerBallSize < 0.11f)
            {
                loseWinState.Lose();
                MoveBall();
            }

            float ballSize = createBall.BallSize();

            if (ballSize >= startPlayerBallSize / 2f)
            {
                MoveBall();
            }

            gameObject.transform.localScale = new Vector3(playerBallSize, playerBallSize, playerBallSize);
            wayBehaviour.WaySize(playerBallSize);
        }
    }
}
