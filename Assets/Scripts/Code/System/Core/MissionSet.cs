using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MissionSet : MonoBehaviour
{
    [SerializeField] private MenuBinder menuBinder;
    [SerializeField] private Image actor;
    [SerializeField] private TextMeshProUGUI actorInfo;
    [SerializeField] private TextMeshProUGUI missionInfo;
    [SerializeField] private TextMeshProUGUI history;
    [SerializeField] private TextMeshProUGUI missionStatus;

    private RectTransform rt;
    private Delivery deliveryRef;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
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
            if (delivery != deliveryRef)
            {
                DOTween.To(() => rt.anchoredPosition, x => rt.anchoredPosition = x, Vector2.right * 600f, .2f)
                       .SetEase(Ease.Linear)
                       .OnComplete(() =>{
                           
                           actor.sprite = delivery.history.actor;
                           actorInfo.text = $"{GameManager.Instance.Translate("Nome")}: {delivery.history.GetName()}\n\n{GameManager.Instance.Translate("Idade")}: {delivery.history.age}\n\n{GameManager.Instance.Translate("Passatempo")}: {delivery.history.GetHobby()}";
                           missionInfo.text = GetStrategyMissionInfo(delivery);
                           history.text = $"{GameManager.Instance.Translate("Historia")}\n\n{delivery.history.GetActorHistory()}";
                           missionStatus.text = $"{GetStatus(delivery.status)}";

                           DOTween.To(() => rt.anchoredPosition, x => rt.anchoredPosition = x, Vector2.zero, 1f).SetEase(Ease.Linear);
                       });
                deliveryRef = delivery;
            }
            else
            {
                missionStatus.text = $"{GetStatus(delivery.status)}";
            }
        }
    }

    private string GetStatus(MissionStatus status)
    {
        switch (status)
        {
            case MissionStatus.COLLECTED:
                return $"<color=green>{GameManager.Instance.Translate("Iniciada")}</color>";
            case MissionStatus.FINISHED:
                return $"<color=green>{GameManager.Instance.Translate("Finalizada")}</color>";
            default:
                return $"<color=red>{GameManager.Instance.Translate("Pendente")}</color>";
        }
    }

    public string GetStrategyMissionInfo(Delivery delivery)
    {
        switch(delivery.history.strategy)
        {
            case DeliveryStrategy.VELOCITY:
                return $"{GameManager.Instance.Translate("Estrategia")}: {GameManager.Instance.Translate("Velocidade")}\n{GameManager.Instance.Translate("Tempo Base")}: {SetTime(delivery.history.baseTime)}\n{GameManager.Instance.Translate("Pontos")}: {delivery.history.points}";
            case DeliveryStrategy.INTIME:
                return $"{GameManager.Instance.Translate("Estrategia")}: {GameManager.Instance.Translate("No Tempo")}\n{GameManager.Instance.Translate("Tempo Base")}: {SetTime(delivery.history.baseTime)}\n{GameManager.Instance.Translate("Pontos")}: {delivery.history.points}";
            case DeliveryStrategy.TEMPERATURE:
                return $"{GameManager.Instance.Translate("Estrategia")}: {GameManager.Instance.Translate("Temperatura")}\n{GameManager.Instance.Translate("Temperatura Base")}: {delivery.history.temperatureReference} %\n{GameManager.Instance.Translate("Pontos")}: {delivery.history.points}";
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

    private void Update()
    {
        actor.enabled = !GameManager.Instance.IsPaused;
        actorInfo.enabled = !GameManager.Instance.IsPaused;
        missionInfo.enabled = !GameManager.Instance.IsPaused;
        history.enabled = !GameManager.Instance.IsPaused;
        missionStatus.enabled = !GameManager.Instance.IsPaused;
    }
}
