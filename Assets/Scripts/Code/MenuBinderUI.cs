using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBinderUI : MonoBehaviour
{
    [SerializeField]
    private MenuBinder menuBinder;

    [SerializeField] private TMPro.TextMeshProUGUI temperature;
    [SerializeField] private TMPro.TextMeshProUGUI control;
    [SerializeField] private TMPro.TextMeshProUGUI battery;
    [SerializeField] private TMPro.TextMeshProUGUI paused;
    [SerializeField] private TMPro.TextMeshProUGUI powerUp;
    [SerializeField] private TMPro.TextMeshProUGUI points;

    private void Start()
    {
        menuBinder.OnTemperatureChange += ChangeTemperature;
        menuBinder.OnControlChange += ChangeControl;
        menuBinder.OnBatteryChange += ChangeBattery;
        menuBinder.OnPausedChange += ChangePaused;
        menuBinder.OnPowerUpChange += ChangePowerUp;
        menuBinder.OnPointsChange += ChangePoints;
    }

    private void ChangePoints(string points)
    {
        if(this.points != null)
            this.points.text = points;
    }

    private void ChangePowerUp(string powerUp)
    {
        if(this.powerUp != null)
            this.powerUp.text = powerUp;
    }

    private void ChangePaused(bool paused)
    {
        if(this.paused != null)
            this.paused.text = paused ? "stop" : "live";
    }

    private void ChangeBattery(string battery)
    {
        if(this.battery != null)
            this.battery.text = battery;
    }

    private void ChangeControl(string control)
    {
        if (this.control != null)
            this.control.text = control;
    }

    private void ChangeTemperature(string temperature)
    {
        if(this.temperature != null)
            this.temperature.text = temperature;
    }

    private void OnDisable()
    {
        menuBinder.OnTemperatureChange -= ChangeTemperature;
        menuBinder.OnControlChange -= ChangeControl;
        menuBinder.OnBatteryChange -= ChangeBattery;
        menuBinder.OnPausedChange -= ChangePaused;
        menuBinder.OnPowerUpChange -= ChangePowerUp;
        menuBinder.OnPointsChange -= ChangePoints;
    }
}
