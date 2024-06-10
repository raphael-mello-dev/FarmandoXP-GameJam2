using Unity.VisualScripting;
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

    public TMPro.TextMeshProUGUI foodTemperature;
    public TMPro.TextMeshProUGUI batteryQuantity;

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
       
        Color batteryColor = Color.HSVToRGB(battery * 0.33f, 1f, 1f);
        batteryQuantity.text = "Battery: <color=#" + ColorToHex(batteryColor) + "> " + (int)(battery * 100) + "%</color>";

 
        Color tempColor = Color.HSVToRGB(0.5f - packageTemperature * 0.5f, 1f, 1f);
        foodTemperature.text = "Temp: <color=#" + ColorToHex(tempColor) + "> " + (int)(packageTemperature * 100) + "°C</color>";
    }
    string ColorToHex(Color color)
    {
        return ((int)(color.r * 255)).ToString("X2") +
               ((int)(color.g * 255)).ToString("X2") +
               ((int)(color.b * 255)).ToString("X2");
    }
}
