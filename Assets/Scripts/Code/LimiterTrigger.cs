using UnityEngine;

public class LimiterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<TriggerInteraction>(out TriggerInteraction component))
        {
            Debug.Log("Game Over");
        }
    }
}
