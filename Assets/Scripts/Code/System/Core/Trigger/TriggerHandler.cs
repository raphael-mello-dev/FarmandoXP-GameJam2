using UnityEngine;

public class TriggerHandler : MonoBehaviour, TriggerInteraction
{
    [SerializeField] private GameObject package;
    [SerializeField] private PlayerData playerData;
    
    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }


    public void OnTrigger(GameObject go, Collider collider)
    {
        switch(go.tag)
        {
            case "Ring":
                ActionStrategy action = new RingStrategy();
                action.Execute(go);
                break;
            case "ReceivePoint":
                playerData.WithPackage = false;
                action = new ReceivePointStrategy();
                action.Execute(package, go);
                break;
            case "CollectPoint":
                playerData.WithPackage = true;
                action = new CollectPointStrategy();
                action.Execute(package, go);
                break;
        }
    }
}
