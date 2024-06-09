using UnityEngine;

public class TriggerHandler : MonoBehaviour, TriggerInteraction
{
    [SerializeField] private GameObject package;

    public void OnTrigger(GameObject go, Collider collider)
    {
        switch(go.tag)
        {
            case "Ring":
                ActionStrategy action = new RingStrategy();
                action.Execute(go);
                break;
            case "ReceivePoint":
                action = new ReceivePointStrategy();
                action.Execute(package, go);
                break;
            case "CollectPoint":
                action = new CollectPointStrategy();
                action.Execute(package, go);
                break;
        }
    }
}
