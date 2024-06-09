using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private PlayerMetaDataSO playerMetaData = null;
    [SerializeField] private float battery = 1;
    [SerializeField] private float packageTemperature = 1;

    private bool withPackage;
    private bool inShadow;

    private PlayerLocomotion playerLocomotion;

    public Slider foodTemperature;
    public Slider batteryQuantity;

    public bool InShadow { get => inShadow; set => inShadow = value; }
    public bool WithPackage { get => withPackage; set => withPackage = value; }

    private void Start()
    {
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        battery = 1;
        packageTemperature = 1;

        if(playerLocomotion == null) {
            Debug.LogError("PlayerDataSO not found!");
        }
    }

    private void Update()
    {
        if (!WithPackage)
        {
            packageTemperature = 1;
        }
        else
        {
            if((playerLocomotion.CurrentSpeed/playerLocomotion.GetMaxSpeed()) < playerMetaData.GetVelocityPercent())
            {
                packageTemperature -= playerMetaData.GetTemperatureDecayRate() * Time.deltaTime;
            }
        }

        if (InShadow)
        {
            battery -= playerMetaData.GetBateryDecayRate() * Time.deltaTime;
        }
        else
        {
            battery += playerMetaData.GetBatteryIncreaseRate() * Time.deltaTime;
        }

        battery = Mathf.Clamp(battery, 0, 1);
        packageTemperature = Mathf.Clamp(packageTemperature, 0, 1);
        batteryQuantity.value = battery;
        foodTemperature.value = packageTemperature;
    }
}
