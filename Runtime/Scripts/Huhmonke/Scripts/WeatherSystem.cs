using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [System.Serializable]
    public struct Weathers
    {
        public string WeatherName;
        [Header("How Long it will last")]
        public float MaxWeatherDuration;
        public float MinWeatherDuration;
        [Header("just drag in rain or snow particles")]
        public GameObject WeatherPrefab;
        [Header("New Skybox Material")]
        public Material SkyboxMaterial;
    }
    [Header("Made By HuhMonke")]
    [Header("Add more weathers")]
    public Weathers[] WeatherSettings;

    [Header("if u got a sphere as skybox fill it here")]
    public Renderer rend;

    private Weathers CurrentWeather;
    private int Index = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartWeather());
    }

    IEnumerator StartWeather()
    {
        while(true)
        {
            Index = Random.Range(0, WeatherSettings.Length);
            CurrentWeather = WeatherSettings[Index];
            if(rend != null && CurrentWeather.SkyboxMaterial != null)
            {
                rend.material = CurrentWeather.SkyboxMaterial;
            }
            if(CurrentWeather.SkyboxMaterial != null)
            {
                RenderSettings.skybox = CurrentWeather.SkyboxMaterial;
            }
            for(int i = 0; i < WeatherSettings.Length; i++)
            {
                if(WeatherSettings[i].WeatherPrefab != null)
                {
                    WeatherSettings[i].WeatherPrefab.SetActive(i == Index);
                }
            }
            yield return new WaitForSeconds(Random.Range(CurrentWeather.MinWeatherDuration, CurrentWeather.MaxWeatherDuration));
        }
    }
}
