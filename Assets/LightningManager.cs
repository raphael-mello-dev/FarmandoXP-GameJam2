using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    [SerializeField] private Vector3 range;
    [SerializeField] private float lightningRange;

    [SerializeField] private float lightningIncidenceTime;
    [SerializeField] private float lightningDelay;
    [SerializeField] private float rangeDivider = 1f;

    [SerializeField] private GameObject lightningWarn;
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private int density;
    private Vector3 upperRange;

    private void Start()
    {
        upperRange = range / rangeDivider;
        for (int i = 0; i < density; i++)
        {
            StartCoroutine(LightningCycle(Random.Range(0f, lightningIncidenceTime)));
        }
    }

    private IEnumerator LightningCycle(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            yield return new WaitForSeconds(lightningIncidenceTime);

            Vector3 warningPosition = GetRandomPositionInRange();
            GameObject warningInstance = Instantiate(lightningWarn, warningPosition, lightningWarn.transform.rotation);

            Destroy(warningInstance, lightningDelay);
            yield return new WaitForSeconds(lightningDelay);

            InstantiateLightning(warningPosition);
        }
    }

    private Vector3 GetRandomPositionInRange()
    {
        return new Vector3(
            Random.Range(-range.x / 2, range.x / 2),
            0.1f,
            Random.Range(-range.z / 2, range.z / 2)
        ) + new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private void InstantiateLightning(Vector3 warningPosition)
    {
        Vector3 endPosition = new Vector3(
            Random.Range(-upperRange.x / 2, upperRange.x / 2),
            upperRange.y,
            Random.Range(-upperRange.z / 2, upperRange.z / 2)
        ) + transform.position;

        GameObject lightningInstance = Instantiate(lightningPrefab);
        LineRenderer lineRenderer = lightningInstance.GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, endPosition);
            lineRenderer.SetPosition(1, warningPosition);
        }
        Destroy(lightningInstance, lightningDelay / 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, range);
    }
}
