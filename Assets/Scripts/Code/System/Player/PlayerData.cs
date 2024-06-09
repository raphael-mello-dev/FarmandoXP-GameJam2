using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float batteryDecayRate = 0.1f;
    [SerializeField] private float batteryIncreaseRate = 0.2f;
    [SerializeField] private float temperatureDecayRate = 0.05f;

    [SerializeField] private float battery = 1;
    [SerializeField] private float packageTemperature = 1;
    [SerializeField, Range(0.0f, 1.0f)] private float velocityPercent = 0.6f;

    private bool withPackage = true;
    private bool inShadow;

    private PlayerLocomotion playerLocomotion;

    public Slider foodTemperature;
    public Slider batteryQuantity;

    public bool InShadow { get => inShadow; set => inShadow = value; }

    private void Start()
    {
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        battery = 1;
        packageTemperature = 1;
    }

    private void Update()
    {
        if (!withPackage)
        {
            packageTemperature = 1;
        }
        else
        {
            if((playerLocomotion.CurrentSpeed/playerLocomotion.GetMaxSpeed()) < velocityPercent)
            {
                packageTemperature -= temperatureDecayRate * Time.deltaTime;
            }
        }

        if (InShadow)
        {
            battery -= batteryDecayRate * Time.deltaTime;
        }
        else
        {
            battery += batteryIncreaseRate * Time.deltaTime;
        }

        battery = Mathf.Clamp(battery, 0, 1);
        packageTemperature = Mathf.Clamp(packageTemperature, 0, 1);
        batteryQuantity.value = battery;
        foodTemperature.value = packageTemperature;
    }
}
