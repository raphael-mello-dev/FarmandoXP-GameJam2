using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    
    public void Setup(float timeToDestroy)
    {
        this.timeToDestroy = timeToDestroy;
    }

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
