using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMetaData", menuName = "FarmandoXP/PlayerMetaData", order = 1)]
public class PlayerMetaDataSO : ScriptableObject
{
    [SerializeField] private float batteryDecayRate = 0.1f;
    [SerializeField] private float batteryIncreaseRate = 0.05f;
    [SerializeField] private float temperatureDecayRate = 0.05f;
    [SerializeField, Range(0.0f, 1.0f)] private float velocityPercent = 0.6f;

    public float GetBateryDecayRate()
    {
        return batteryDecayRate;
    }
    
    public float GetBatteryIncreaseRate()
    {
        return batteryIncreaseRate;
    }

    public float GetTemperatureDecayRate()
    {
        return temperatureDecayRate;
    }

    public float GetVelocityPercent()
    {
        return velocityPercent;
    }
}
