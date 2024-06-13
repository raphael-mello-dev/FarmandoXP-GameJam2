using UnityEngine;

public class PlayerWindZone : MonoBehaviour
{
    [SerializeField] private Vector3 wind;
    PlayerLocomotion playerLocomotion = null;
    CharacterController playerController = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AudioManager.PlayWindSFX();
            playerLocomotion = other.GetComponent<PlayerLocomotion>();
            playerController = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AudioManager.StopWindSFX();
            if (playerLocomotion != null)
            {
                playerLocomotion.ExternalMove = Vector3.zero;
            }
            playerLocomotion = null;
            playerController = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerLocomotion != null && playerController != null)
            {
                if (playerController.velocity.magnitude > wind.magnitude)
                {
                    playerLocomotion.ExternalMove = Vector3.Lerp(playerLocomotion.ExternalMove, wind, 0.9f);
                }
                else
                {
                    playerLocomotion.ExternalMove = -playerController.velocity;
                }
            }
        }
    }
}
