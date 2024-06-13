using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionSet : MonoBehaviour
{
    [SerializeField] private MenuBinder menuBinder;
    [SerializeField] private Image actor;
    [SerializeField] private TextMeshProUGUI actorInfo;
    [SerializeField] private TextMeshProUGUI missionInfo;
    [SerializeField] private TextMeshProUGUI history;
    [SerializeField] private TextMeshProUGUI missionStatus;

    private void Awake()
    {
        menuBinder.OnDeliveryChange += OnDeliveryChange;
    }

    private void OnDisable()
    {
        menuBinder.OnDeliveryChange -= OnDeliveryChange;
    }

    private void OnDeliveryChange(Delivery delivery)
    {
        if(delivery != null)
        {
            actor.sprite = delivery.history.actor;
            actorInfo.text = $"Nome: {delivery.history.name}\n\nIdade: {delivery.history.age}\n\nPassatempo: {delivery.history.hobby}";
            missionInfo.text = GetStrategyMissionInfo(delivery);
            history.text = $"Historia\n\n{delivery.history.actorHistory}";
            missionStatus.text = $"{GetStatus(delivery.status)}";
        }
    }

    private string GetStatus(MissionStatus status)
    {
        switch (status)
        {
            case MissionStatus.COLLECTED:
                return "<color=green>Iniciada</color>";
            case MissionStatus.FINISHED:
                return "<color=green>Finalizada</color>";
            default:
                return "<color=red>Pendente</color>";
        }
    }

    public string GetStrategyMissionInfo(Delivery delivery)
    {
        switch(delivery.history.strategy)
        {
            case DeliveryStrategy.VELOCITY:
                return $"Estrategia: Velocidade\nTempo Base: {SetTime(delivery.history.baseTime)}\nPontos: {delivery.history.points}";
            case DeliveryStrategy.INTIME:
                return $"Estrategia: No Tempo\nTempo Base: {SetTime(delivery.history.baseTime)}\nPontos: {delivery.history.points}";
            case DeliveryStrategy.TEMPERATURE:
                return $"Estrategia: Temperatura\nTemperatura Base: {delivery.history.temperatureReference} %\nPontos: {delivery.history.points}";
            default:
                return "Livre";
        }
    }

    private string SetTime(int baseTime)
    {
        int minutes = baseTime / 60;
        int seconds = baseTime % 60;
        return string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

}
