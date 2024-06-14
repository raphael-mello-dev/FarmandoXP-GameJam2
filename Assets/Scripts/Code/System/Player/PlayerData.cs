using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour, IPowerUpActor
{
    [SerializeField] private PlayerMetaDataSO playerMetaData = null;
    [SerializeField] private float battery = 1;
    [SerializeField] private float packageTemperature = 1;
    private bool triggerInvert;

    private bool withPackage;
    private bool inShadow;
    private bool isInverted;
    private float invertControlTime;
    private bool hasPowerUp;
    private float powerUpTimeRemaining;
    private bool withAmplifier;
    private bool withVelocityMultiplier;
    private bool withDragAir;

    private PoweUpData currentPowerUpData;
    private PoweUPType currentPowerUpType;

    [SerializeField] private float invertControlDuration;

    private PlayerLocomotion playerLocomotion;
    private Orchestrator orchestrator;

    [SerializeField] private MenuBinder menuBinder;

    public bool InShadow { get => inShadow; set => inShadow = value; }
    public bool WithPackage { get => withPackage; set => withPackage = value; }
    public bool IsInverted { get => isInverted; private set => isInverted = value; }
    public bool TriggerInvert { get => triggerInvert; set => triggerInvert = value; }
    public bool WithAmplifier { get => withAmplifier; set => withAmplifier = value; }
    public bool WithVelocityMultiplier { get => withVelocityMultiplier; set => withVelocityMultiplier = value; }
    public bool WithDragAir { get => withDragAir; set => withDragAir = value; }

    private void Start()
    {
        orchestrator = FindAnyObjectByType<Orchestrator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        battery = 1;
        packageTemperature = 1;

        if (playerLocomotion == null)
        {
            Debug.LogError("PlayerLocomotion not found!");
        }

        if (orchestrator == null)
        {
            Debug.LogError("Orchestrator not found!");
        }
    }
 
    private void Update()
    {
        WithPackage = orchestrator.WithPackage();
        if (!WithPackage)
        {
            packageTemperature = 1;
        }
        else
        {
            if ((playerLocomotion.CurrentSpeed / playerLocomotion.GetMaxSpeed()) < playerMetaData.GetVelocityPercent())
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

        if (battery < 0.3f)
        {
            GameManager.Instance.AudioManager.PlayAlarmSFX();
        }
        else
        {
            GameManager.Instance.AudioManager.StopAlarmSFX();
        }

        if(battery  == 0f || packageTemperature == 0f)
        {
            GameManager.Instance.GameStateMachine.SwitchState<GameOverState>();
            SceneManager.LoadScene(2);
        }

        UpdateUI();
        HandleControlInversion();
        HandlePowerUp();
    }

    private void UpdateUI()
    {
        Color batteryColor = Color.HSVToRGB(battery * 0.33f, 1f, 1f);
        menuBinder.Battery = $"Battery: <color=#{ColorToHex(batteryColor)}> {(int)(battery * 100)}%</color>";

        Color tempColor = Color.HSVToRGB(0.5f - packageTemperature * 0.5f, 1f, 1f);
        menuBinder.Temperature = $"Temp: <color=#{ColorToHex(tempColor)}> {(int)(packageTemperature * 100)}%</color>";

        string controlText = (IsInverted || TriggerInvert) && !WithAmplifier ? "Inverted" : "Good";
        Color controlColor = (IsInverted || TriggerInvert) && !WithAmplifier ? Color.HSVToRGB(0f, 1f, 1f) : Color.HSVToRGB(0.33f, 1f, 1f);
        menuBinder.Control = $"Control: <color=#{ColorToHex(controlColor)}> {controlText}</color>";
        menuBinder.Paused = GameManager.Instance.IsPaused;
        menuBinder.Points = $"Points: <color=yellow> {(int)orchestrator.Points}</color>";
    }

    private void HandleControlInversion()
    {
        if (IsInverted)
        {
            invertControlTime += Time.deltaTime;
            if (invertControlTime > invertControlDuration)
            {
                IsInverted = false;
                invertControlTime = 0;
            }
        }
    }

    public int GetTemperaturePercent()
    {
        return (int)(packageTemperature * 100);
    }

    private void HandlePowerUp()
    {
        if (hasPowerUp)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            if (powerUpTimeRemaining <= 0)
            {
                hasPowerUp = false;
                DeactivatePowerUp();
            }
        }
    }

    private void DeactivatePowerUp()
    {
        switch (currentPowerUpType)
        {
            case PoweUPType.VELOCITY:
                playerLocomotion.VelocityMultiplier = 1f;
                WithVelocityMultiplier = false;
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(0f, 1f, 1f))}> None</color>";
                break;
            case PoweUPType.AMPLIFIER:
                WithAmplifier = false;
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(0f, 1f, 1f))}> None</color>";
                break;
            case PoweUPType.DRAG_AIR:
                WithDragAir = false;
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(0f, 1f, 1f))}> None</color>";
                break;
        }
    }

    public void InvertControl(float time)
    {
        IsInverted = true;
        invertControlDuration = time;
        invertControlTime = 0;
    }

    private string ColorToHex(Color color)
    {
        return ((int)(color.r * 255)).ToString("X2") +
               ((int)(color.g * 255)).ToString("X2") +
               ((int)(color.b * 255)).ToString("X2");
    }

    public void OnPoweUp(PoweUPType type, PoweUpData data)
    {
        if (hasPowerUp)
        {
            DeactivatePowerUp(); // Desativa o power-up atual antes de ativar o novo
        }

        hasPowerUp = true;
        currentPowerUpType = type;
        currentPowerUpData = data;
        powerUpTimeRemaining = data.timePower;

        switch (type)
        {
            case PoweUPType.VELOCITY:
                playerLocomotion.VelocityMultiplier = currentPowerUpData.velocityMultiplier;
                WithVelocityMultiplier = true;
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(.33f, 1f, 1f))}> Velocity</color>";
                break;
            case PoweUPType.TEMPERATURE:
                packageTemperature = Mathf.Clamp01(packageTemperature + currentPowerUpData.temperatureIncrease/100f);
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(.33f, 1f, 1f))}> Temperature</color>";
                break;
            case PoweUPType.AMPLIFIER:
                WithAmplifier = true;
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(.33f, 1f, 1f))}> Amplifier</color>";
                break;
            case PoweUPType.DRAG_AIR:
                menuBinder.PowerUP = $"PowerUp: <color=#{ColorToHex(Color.HSVToRGB(.33f, 1f, 1f))}> Air Drag</color>";
                WithDragAir = true;
                break;
        }
    }

    public bool CanInvert()
    {
        return (IsInverted || TriggerInvert) && !WithAmplifier;
    }
}
