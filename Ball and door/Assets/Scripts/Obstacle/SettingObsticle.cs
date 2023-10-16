using System.Collections;
using UnityEngine;

public class SettingObsticle : MonoBehaviour
{
    [SerializeField] private float yPosition;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        StartCoroutine(DelayToCollider());
    }

    private IEnumerator DelayToCollider()
    {
        yield return new WaitForSeconds(1f);
        gameObject.AddComponent<BoxCollider>();
    }
}
