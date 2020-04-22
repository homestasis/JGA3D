using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhetherController : MonoBehaviour
{
    [SerializeField] private SunLightController sunLight;
    [SerializeField] private Rain3DController rain;

    internal void BeRainny()
    {
        rain.StartToSoundRain();
        sunLight.Darken();
    }

    internal void BeSunny()
    {

    }
}
