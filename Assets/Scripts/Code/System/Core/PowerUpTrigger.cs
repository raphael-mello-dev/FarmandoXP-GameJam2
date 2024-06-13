using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUpActor
{
    void OnPoweUp(PoweUPType type, PoweUpData data);
}

public enum PoweUPType
{
   VELOCITY, TEMPERATURE, AMPLIFIER, DRAG_AIR
}

[System.Serializable]
public class PoweUpData
{
    public float velocityMultiplier;
    public float timePower;
    public float temperatureIncrease;
}

public class PowerUpTrigger : MonoBehaviour
{
    public PoweUPType type;
    public PoweUpData powerUpInfo;
    public bool destroyAfterActivate = true;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IPowerUpActor component))
        {
            component.OnPoweUp(type, powerUpInfo);
            if (destroyAfterActivate)
            {
                Destroy(gameObject);
            }
        }   
    }

}
