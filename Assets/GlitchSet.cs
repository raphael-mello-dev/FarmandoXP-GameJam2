using UnityEngine;

public class GlitchSet : MonoBehaviour
{
    [SerializeField] private Material mat;
    [Range(0, 1f)][SerializeField] private float scanLinesStretch = 1f;
    [SerializeField] private float noiseAmount = 0f;
    [SerializeField] private float glitchForce = 0f;

    private float time;

    private void Start()
    {
        mat.SetFloat("_ScanLinesStretch", scanLinesStretch);
        mat.SetFloat("_NoiseAmount", noiseAmount);
        mat.SetFloat("_GlitchForce", glitchForce);
    }

    public void AddGlitch(float noise, float force, float duration)
    {
        mat.SetFloat("_NoiseAmount", noise);
        mat.SetFloat("_GlitchForce", force);
        time = duration;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddGlitch(100f, 5f, 1f);
        }
    }

    private void FixedUpdate()
    {

        if (time > 0)
        {
            time -= Time.fixedDeltaTime;
            if (time <= 0)
            {
                // Reset the values once the glitch time is over
                mat.SetFloat("_NoiseAmount", noiseAmount);
                mat.SetFloat("_GlitchForce", glitchForce);
            }
        }
    }
}
