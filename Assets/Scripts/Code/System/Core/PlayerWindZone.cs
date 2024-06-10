using UnityEngine;

public class PlayerWindZone : MonoBehaviour
{
    private GameObject player;

    //Global Direction of Wind
    [SerializeField] private Vector3 wind;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player = (GameObject)other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerLocomotion>().ExternalMove = Vector2.zero;
            player = null;
        }
    }

    private void Update()
    {
        if(player != null)
        {
            player.GetComponent<PlayerLocomotion>().ExternalMove = wind;
        }
    }
}
