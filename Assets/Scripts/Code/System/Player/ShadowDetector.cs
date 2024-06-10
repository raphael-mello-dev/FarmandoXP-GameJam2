using UnityEngine;

public class ShadowDetector : MonoBehaviour
{
    private GameObject directionalLight;
    private PlayerData playerData;

    private void Start()
    {
        directionalLight = GameObject.FindGameObjectWithTag("Light");
        playerData = GetComponent<PlayerData>();
    }

    void Update()
    {
        Vector3 lightDirection = directionalLight.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -lightDirection, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != gameObject)
            {
                Debug.Log("0");
                playerData.InShadow = true;
            }
            else
            {
                Debug.Log("1");
                playerData.InShadow = false;
            }
        }
        else
        {
            Debug.Log("2");
            playerData.InShadow = false;
        }
    }
}
