using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Transform ball;

    [SerializeField] private Animator rightDoor;
    [SerializeField] private Animator leftDoor;

    private void Start()
    {
        DoorState(false);
    }

    private void Update()
    {
        if(Vector3.Distance(ball.position, transform.position) < 7f)
        {
            DoorState(true);
        }
    }

    private void DoorState(bool open)
    {
        rightDoor.enabled = open;
        leftDoor.enabled = open;
    }
}
