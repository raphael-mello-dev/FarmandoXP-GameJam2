using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AudioManager.PlayRainSFX();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AudioManager.StopRainSFX();
        }
    }
}
