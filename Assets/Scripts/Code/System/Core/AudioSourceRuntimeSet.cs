using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioSourceRuntimeSet : MonoBehaviour
{
    [SerializeField] private AudioSourceSO audioSourceSO;
    private AudioSource audioSource;

    private void Awake()
    {
        if(audioSourceSO == null) { 
            Debug.LogError("AudioSourceSO not found!");
        }
        
        audioSource = GetComponent<AudioSource>();

        audioSourceSO.Setup(audioSource);
    }
}
