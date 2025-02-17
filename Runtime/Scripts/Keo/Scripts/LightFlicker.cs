using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    [Header("Script made by Keo.cs :)")]
    [Header("No need for credits")]
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;

    private Light lightSource;
    private float targetIntensity;
    private float changeSpeed;

    void Start()
    {
        lightSource = GetComponent<Light>();
        SetNewTarget();
    }

    void Update()
    {
        lightSource.intensity = Mathf.MoveTowards(lightSource.intensity, targetIntensity, changeSpeed * Time.deltaTime);

        if (Mathf.Approximately(lightSource.intensity, targetIntensity))
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        changeSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
