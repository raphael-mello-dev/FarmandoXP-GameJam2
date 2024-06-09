using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float temperatureDecayRate = 0.1f;
    [SerializeField] private float temperatureIncreaseRate = 0.2f;
    [SerializeField] private float batteryDecayRateInShadow = 0.05f;

    [SerializeField] private float batery = 1;
    [SerializeField] private float packageTemperature = 1;

    private bool withPackage = true;
    private bool inShadow;

    private PlayerLocomotion playerLocomotion;

    public Slider foodTemperature;

    private void Start()
    {
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        batery = 1;
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
            // Decai a temperatura do pacote ao longo do tempo
            packageTemperature -= temperatureDecayRate * Time.deltaTime;

            // Penalidade mínima de temperatura quando o jogador está se movendo
            float speedPenalty = playerLocomotion.CurrentSpeed > 0 ? temperatureIncreaseRate * Time.deltaTime : 0;

            // Aumenta a temperatura do pacote com base na velocidade atual do jogador
            packageTemperature += (playerLocomotion.CurrentSpeed * temperatureIncreaseRate + speedPenalty) * Time.deltaTime;
        }

        // Se estiver na sombra, a bateria decai ao longo do tempo
        if (inShadow)
        {
            batery -= batteryDecayRateInShadow * Time.deltaTime;
        }

        // Mantém os valores dentro dos limites [0, 1]
        packageTemperature = Mathf.Clamp(packageTemperature, 0, 1);
        batery = Mathf.Clamp(batery, 0, 1);
        foodTemperature.value = packageTemperature;
    }

}
