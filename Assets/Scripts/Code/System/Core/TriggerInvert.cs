using UnityEngine;

public class TriggerInvert : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out TriggerHandler component))
        {
            component.OnTriggerInvert(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TriggerHandler component))
        {
            component.OnTriggerInvert(false);
        }
    }
}
