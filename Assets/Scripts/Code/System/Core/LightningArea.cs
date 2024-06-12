using UnityEngine;
using System.Collections.Generic;

public class LightningArea : MonoBehaviour
{
    [SerializeField] private float sizeX;
    [SerializeField] private float sizeZ;

    [SerializeField] private GameObject lightningPrefab;

    [SerializeField] private float lightningIncidenceTime;

    private List<CreateLightning> rainInstances = new List<CreateLightning>();
    private Queue<CreateLightning> rainPool = new Queue<CreateLightning>();

    private void Start()
    {
        // Inicializa o object pooling
        for (int i = 0; i < 10; i++) // Número inicial de raios pré-instanciados
        {
            CreateLightning rain = Instantiate(lightningPrefab, transform.position, Quaternion.identity).GetComponent<CreateLightning>();
            rain.gameObject.SetActive(false);
            rainPool.Enqueue(rain);
        }

        // Começa a gerar raios de maneira contínua
        InvokeRepeating("SpawnLightning", 0f, lightningIncidenceTime);
    }

    private void SpawnLightning()
    {
        // Remove instâncias antigas do pool, se houver
        if (rainInstances.Count > 0 && rainInstances[0].IsFinished)
        {
            CreateLightning oldRain = rainInstances[0];
            oldRain.gameObject.SetActive(false);
            rainInstances.RemoveAt(0);
            rainPool.Enqueue(oldRain);
        }

        CreateLightning newRain;
        if (rainPool.Count > 0)
        {
            newRain = rainPool.Dequeue();
        }
        else
        {
            newRain = Instantiate(lightningPrefab, transform.position, Quaternion.identity).GetComponent<CreateLightning>();
        }

        newRain.transform.position = RandomPositionWithinArea();
        newRain.gameObject.SetActive(true);
        newRain.Init(ReturnRainToPool);
        rainInstances.Add(newRain);
    }

    private Vector3 RandomPositionWithinArea()
    {
        float randomX = Random.Range(-sizeX / 2f, sizeX / 2f);
        float randomZ = Random.Range(-sizeZ / 2f, sizeZ / 2f);
        return new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }

    private void ReturnRainToPool(CreateLightning rain)
    {
        rain.gameObject.SetActive(false);
        rainInstances.Remove(rain);
        rainPool.Enqueue(rain);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, 1f, sizeZ));
    }
}
