using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DigitalRuby.RainMaker;

public class Rain3DController : MonoBehaviour
{

    [SerializeField] private float delta;
    private RainScript rain;

    private float minRainInt;
    private float maxRainInt;
    
    private void Awake()
    {
        rain = this.GetComponent<RainScript>();
        rain.RainIntensity = 0.1f;
    }

    private void Start()
    {
        minRainInt = 0.1f;
        maxRainInt = 0.4f;

    }
    internal async void StartToSoundRain()
    {
        if (rain.RainIntensity >= maxRainInt) { return; }
        while (true)
        {
            rain.RainIntensity += delta;
            if (rain.RainIntensity >= maxRainInt)
            {
                rain.RainIntensity = maxRainInt;
                return;
            }

            await Task.Delay(50);
        }
    }

    internal async void StopToSoundRain()
    {
        if(rain.RainIntensity <= minRainInt) { return; }
        while (true)
        {
            rain.RainIntensity -= delta;
            if (rain.RainIntensity <= minRainInt)
            {
                rain.RainIntensity = minRainInt;
                return;
            }

            await Task.Delay(50);
        }
    }

    
}
