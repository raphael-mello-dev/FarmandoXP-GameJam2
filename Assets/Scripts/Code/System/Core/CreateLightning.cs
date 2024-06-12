using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CreateLightning : MonoBehaviour
{
    [Range(2, 8)]
    [SerializeField] private int quantityPoints = 5;
    [SerializeField] private float radiusNoise = 0.1f;
    [SerializeField] private float burstTime = 0.5f;
    [SerializeField] private float width = 0.1f;
    [SerializeField] private float maxWidth = 0.5f;
    [SerializeField] private GameObject lightningWarn;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private bool enableAnimation = true;
    [SerializeField] private float burstIntensity = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float sphereRadius;

    [ColorUsage(true, true)]
    public Color color = Color.blue;
    private Vector3[] points;
    private LineRenderer lineRenderer;
    private float height = 5.0f;
    private bool isFinished = true;
    private float burstTimer = 0f;
    private UnityAction<CreateLightning> OnFinish;
    
    public bool IsFinished { get => isFinished; set => isFinished = value; }

    public void Init(UnityAction<CreateLightning> OnFinish)
    {
        isFinished = false;
        this.OnFinish = OnFinish;
        if (quantityPoints < 2) quantityPoints = 2;
        height = transform.position.y;

        lightningWarn.SetActive(true);
        lightningWarn.transform.localPosition = new Vector3(0f, -transform.localPosition.y + 0.1f, 0f);
        StartCoroutine(DelayAndStart());
    }

    private IEnumerator DelayAndStart()
    {
        yield return new WaitForSeconds(delay);
        StartLightning();
    }

    private void StartLightning()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = quantityPoints;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        points = new Vector3[quantityPoints];

        GeneratePoints();

        lineRenderer.SetPositions(points);

        lineRenderer.material.SetColor("_Color", color);
        Burst();
    }

    public void Burst()
    {
        StartCoroutine(BurstCoroutine());
    }

    private IEnumerator BurstCoroutine()
    {
        Material lightningWarnMaterial = lightningWarn.GetComponent<SpriteRenderer>().material;
        Color lightningWarnMaterialColorStart =  lightningWarnMaterial.GetColor("_Color");
        
        GameManager.Instance.AudioManager.PlaySFXAtPoint(SFXs.Thunder, lightningWarn.transform);
        
        yield return new WaitUntil(() => gameObject.activeSelf); // Aguarda até que o objeto esteja ativo
        burstTimer = 0f;
        while (burstTimer < burstTime)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out hit, Mathf.Infinity, playerLayer))
            {
                if(hit.collider.gameObject.TryGetComponent<TriggerHandler>(out TriggerHandler component))
                {
                    component.OnTrigger(this.gameObject, hit.collider);
                }
            }
            burstTimer += Time.deltaTime;
            float emissionIntensity = Mathf.PingPong(burstTimer / burstTime, 1.0f) * burstIntensity;
            lightningWarnMaterial.SetColor("_Color", lightningWarnMaterialColorStart * emissionIntensity);

            Color finalEmissionColor = color * emissionIntensity;
            lineRenderer.material.SetColor("_Color", finalEmissionColor);

            yield return null;
        }
        lineRenderer.material.SetColor("_Color", lightningWarnMaterialColorStart);
        lightningWarnMaterial.SetColor("_Color", color);

        lineRenderer.positionCount = 0;
        this.OnFinish.Invoke(this);
        lightningWarn.SetActive(false);
        isFinished = true;
    }

    void Update()
    {
        if (isFinished || lineRenderer == null || lineRenderer.positionCount != quantityPoints) return;
        if (!enableAnimation) return;

        if (points == null)
        {
            points = new Vector3[quantityPoints];
            GeneratePoints();
        }

        for (int i = 1; i < quantityPoints - 1; i++)
        {
            float noiseX = Random.Range(-radiusNoise, radiusNoise);
            float noiseZ = Random.Range(-radiusNoise, radiusNoise);
            float yPos = transform.position.y - (height / (quantityPoints - 1)) * i;

            points[i] = new Vector3(transform.position.x + noiseX, yPos, transform.position.z + noiseZ);
        }

        lineRenderer.SetPositions(points);
    }


    private void GeneratePoints()
    {
        points[0] = transform.position;

        for (int i = 1; i < quantityPoints - 1; i++)
        {
            float noiseX = Random.Range(-radiusNoise, radiusNoise);
            float noiseZ = Random.Range(-radiusNoise, radiusNoise);
            float yPos = transform.position.y - (height / (quantityPoints - 1)) * i;
            points[i] = new Vector3(transform.position.x + noiseX, yPos, transform.position.z + noiseZ);
        }

        points[quantityPoints - 1] = new Vector3(transform.position.x, transform.position.y - height, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
