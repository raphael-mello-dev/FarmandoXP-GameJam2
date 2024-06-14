using UnityEngine;

internal interface TriggerInteraction
{
    void OnTrigger(GameObject go, Collider collider);
    void OnTrigger(float multiplier);
    void OnTriggerInvert(bool value);
}
