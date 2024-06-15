using UnityEngine;

public class TriggerHandler : MonoBehaviour, TriggerInteraction
{
    private PlayerData playerData;
    private GlitchSet glitchSet;

    private void Start()
    {
        glitchSet = FindAnyObjectByType<GlitchSet>();
        playerData = GetComponent<PlayerData>();
    }

    public void OnTrigger(GameObject go, Collider collider){
        playerData.InvertControl(5f);
    }

    public void OnTriggerInvert(bool value)
    {
        playerData.TriggerInvert = value;
    }

    public void OnTrigger(float multiplier)
    {
        if (multiplier < 0.2f) return;
        playerData.InvertControl(5f * multiplier);
    }
}
