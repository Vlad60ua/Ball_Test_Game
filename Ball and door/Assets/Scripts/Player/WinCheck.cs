using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WinZone"))
        {
            LoseWinState.instance.Win();
        }
    }
}
