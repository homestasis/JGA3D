using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class WeatherController : MonoBehaviour
{
    [SerializeField] private GameObject sunLightOb;
    [SerializeField] private GameObject rainPf;

    private SunLightController sunLight;
    private Rain3DController rain;

    private bool isNormalRainy;
    private bool isHeavyRainy;

    private void Awake()
    {
        sunLight = sunLightOb.GetComponent<SunLightController>();
        rain = rainPf.GetComponent<Rain3DController>();
    }

    internal bool GetIsNormalRainy() { return isNormalRainy; }
    internal bool GetIsHeavyRainy() { return isHeavyRainy;  }

    internal async Task BeRainnyAsync()
    {
        sunLight.Darken();
        await Task.Delay(1200);
        rain.StartToSoundRain();
    }

    internal void BeSunny()
    {
        sunLight.Lighten();
        rain.StopToSoundRain();
    }
}
