using UnityEngine;
using UnityEngine.Events;

public class TriggerActor : MonoBehaviour
{
    public UnityAction<GameObject, Collider> OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<TriggerInteraction>(out TriggerInteraction component))
        {
            OnTrigger?.Invoke(this.gameObject, other);
            //component.OnTrigger(this.gameObject, other);
        }
    }
}
