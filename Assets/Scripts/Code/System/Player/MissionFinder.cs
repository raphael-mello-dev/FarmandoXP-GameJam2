using UnityEngine;

public class MissionFinder : MonoBehaviour
{
    private PlayerLocomotion playerLocomotion;
    private Orchestrator orchestrator;
    [SerializeField] private GameObject finder;
    void Start()
    {
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        transform.parent = null;
        orchestrator = FindAnyObjectByType<Orchestrator>();
    }

    void Update()
    {
        GameObject target = orchestrator.GetNextTarget();
        if (target != null)
        {
            Vector3 direction = (target.transform.position - playerLocomotion.transform.position).normalized;

            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
            finder.SetActive(true);
        }
        else
        {
            finder.SetActive(false);
        }
        transform.position = playerLocomotion.transform.position;
    }
}
