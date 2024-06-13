using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuBinder", menuName = "FarmandoXP/MenuBinder", order = 0)]
public class MenuBinder : ScriptableObject
{
    private string temperature;
    private string control;
    private string battery;
    private string powerUp;
    private string points;
    private Delivery delivery;

    private bool paused;
    private float timeDelivery;

    public event Action<string> OnTemperatureChange;
    public event Action<string> OnControlChange;
    public event Action<string> OnBatteryChange;
    public event Action<string> OnPowerUpChange;
    public event Action<string> OnPointsChange;
    public event Action<Delivery> OnDeliveryChange;
    public event Action<bool> OnPausedChange;

    public string Temperature
    {
        get => temperature;
        set
        {
            //if (temperature != value)
            {
                temperature = value;
                OnTemperatureChange?.Invoke(temperature);
            }
        }
    }

    public string Control
    {
        get => control;
        set
        {
            if (control != value)
            {
                control = value;
                OnControlChange?.Invoke(control);
            }
        }
    }

    public string Battery
    {
        get => battery;
        set
        {
            if (battery != value)
            {
                battery = value;
                OnBatteryChange?.Invoke(battery);
            }
        }
    }

    public bool Paused
    {
        get => paused;
        set
        {
            if (paused != value)
            {
                paused = value;
                OnPausedChange?.Invoke(paused);
            }
        }
    }

    public string PowerUP
    {
        get => powerUp;
        set
        {
            if (powerUp != value)
            {
                powerUp = value;
                OnPowerUpChange?.Invoke(powerUp);
            }
        }
    }

    public string Points
    {
        get => points;
        set
        {
            if (points != value)
            {
                points = value;
                OnPointsChange?.Invoke(points);
            }
        }
    }

    public Delivery Delivery
    {
        get => delivery;
        set
        {
            //if (delivery != value)
            {
                delivery = value;
                OnDeliveryChange?.Invoke(delivery);
            }
        }
    }

    public float TimeDelivery { get => timeDelivery; set => timeDelivery = value; }
}
