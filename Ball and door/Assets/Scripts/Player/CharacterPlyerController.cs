using System.Collections;
using UnityEngine;

public class CharacterPlyerController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float maxSpeedMove = 20f;
    [SerializeField] private float distanceToStopGate = 5f;

    [SerializeField] private Transform direction;

    [SerializeField] private float timeToDelayJump = 1f;

    [HideInInspector] public bool Jump;

    private float gravityStrength = 10f;

    private Vector3 velocity;

    private Coroutine moveCoroutine;

    private bool jumpStop;

    private LoseWinState loseWinState;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        loseWinState = LoseWinState.instance;
    }

    private void FixedUpdate()
    {
        if (loseWinState.winJump && !Jump)
        {
            StartMoveCoroutine(true);
        }

        if (!Jump && !loseWinState.stopGame)
        {
            RaycastHit hit;
            Vector3 castOrigin = direction.transform.position;
            Vector3 castDirection = direction.transform.forward;

            if (Physics.SphereCast(castOrigin, gameObject.transform.localScale.x, castDirection, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Gate"))
                {
                    maxDistance = distanceToStopGate;
                }
                else if (hit.transform.CompareTag("WinZone"))
                {
                    CanJump();
                }
            }
            else
            {
                CanJump();
            }
        }
        else if (jumpStop)
        {
            CheckJumpStop();
        }
    }

    private void CanJump()
    {
        PlayJumpSound();
        StartMoveCoroutine(false);
        Jump = true;
        DelayStopJump();
    }

    private void StartMoveCoroutine(bool winJump)
    {
        if (winJump)
        {
            velocity = transform.forward * 20f;
        }
        else
        {
            velocity = transform.forward * maxSpeedMove;
        }
        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(SoundManager.instance.GetSound(true));
    }

    private void DelayStopJump()
    {
        StartCoroutine(DelayToStopJump());
    }

    private void CheckJumpStop()
    {
        if (transform.position.y < (transform.localScale.x / 2) + 0.2f)
        {
            StopCoroutine(moveCoroutine);
            DelayToJump();
            jumpStop = false;
        }
    }

    private IEnumerator DelayToStopJump()
    {
        yield return new WaitForSeconds(timeToDelayJump);
        jumpStop = true;
    }

    private void DelayToJump()
    {
        StartCoroutine(DelayToJumpCoroutine());
    }

    private IEnumerator DelayToJumpCoroutine()
    {
        yield return new WaitForSeconds(timeToDelayJump / 2);
        Jump = false;
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            velocity.y -= gravityStrength * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
