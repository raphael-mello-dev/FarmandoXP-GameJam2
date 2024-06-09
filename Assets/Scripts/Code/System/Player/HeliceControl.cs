using UnityEngine;

public class HeliceControl : MonoBehaviour
{
    [SerializeField] private float rotationVelocity = 10f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationVelocity * Time.deltaTime);
    }
}
