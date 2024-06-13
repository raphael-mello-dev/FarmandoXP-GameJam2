
using UnityEngine;

[System.Serializable]
public class Delivery
{
    public TriggerActor collectPoint;
    public TriggerActor receivePoint;
    public GameObject package;
    public MissionStatus status = MissionStatus.PENDING;
    public DeliveryHistory history;
}
