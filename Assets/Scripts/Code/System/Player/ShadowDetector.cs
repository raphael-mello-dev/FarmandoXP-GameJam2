using UnityEngine;

public class ShadowDetector : MonoBehaviour
{
    private GameObject directionalLight;
    private PlayerData playerData;
    [SerializeField] private LayerMask lm;
    private void Start()
    {
        directionalLight = GameObject.FindGameObjectWithTag("Light");
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        Vector3 lightDirection = directionalLight.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -lightDirection, out hit, Mathf.Infinity, lm))
        {
            if (hit.collider.gameObject != gameObject)
            {
                playerData.InShadow = true;
            }
            else
            {
                playerData.InShadow = false;
            }
        }
        else
        {
            playerData.InShadow = false;
        }
    }
}
