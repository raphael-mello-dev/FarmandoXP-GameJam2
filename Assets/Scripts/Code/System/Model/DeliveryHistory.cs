using UnityEngine;

public enum DeliveryStrategy
{
    VELOCITY,
    INTIME,
    TEMPERATURE
}

[System.Serializable]
public class DeliveryHistory
{
    public Sprite actor;
    public string name;
    public int age;
    public string hobby;
    public DeliveryStrategy strategy = DeliveryStrategy.VELOCITY;
    public int points;
    public int baseTime;
    public int temperatureReference;
    [TextAreaAttribute] public string actorHistory;
}