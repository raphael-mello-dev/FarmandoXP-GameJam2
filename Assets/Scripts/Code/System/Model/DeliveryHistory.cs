using UnityEngine;

public enum DeliveryStrategy
{
    VELOCITY,
    INTIME,
    TEMPERATURE
}

[System.Serializable]
public class DeliveryHistory
{
    public Sprite actor;
    public string name, name_en;
    public int age;
    public string hobby, hobby_en;
    public DeliveryStrategy strategy = DeliveryStrategy.VELOCITY;
    public int points;
    public int baseTime;
    public int temperatureReference;
    [TextArea] public string actorHistory, actorHistory_en;

    public string GetName() {
        return GameManager.Instance.selectedLanguage == Languagues.Portuguese ? name : name_en; 
    }

    public string GetHobby()
    {
        return GameManager.Instance.selectedLanguage == Languagues.Portuguese ? hobby : hobby_en;
    }

    public string GetActorHistory()
    {
        return GameManager.Instance.selectedLanguage == Languagues.Portuguese ? actorHistory : actorHistory_en;
    }
}