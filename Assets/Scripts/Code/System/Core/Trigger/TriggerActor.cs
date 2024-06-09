using UnityEngine;

public class TriggerActor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<TriggerInteraction>(out TriggerInteraction component))
        {
            component.OnTrigger(this.gameObject, other);
        }
    }
}
