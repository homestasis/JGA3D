using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DigitalRuby.RainMaker;

public class Rain3DController : MonoBehaviour
{

    [SerializeField] private float delta;
    private RainScript rain;
    
    
    private void Awake()
    {
        rain = this.GetComponent<RainScript>();
        rain.RainIntensity = 0.1f;
    }

    internal async void StartToSoundRain()
    {
        if (rain.RainIntensity >= 0.41f) { return; }
        while (true)
        {
            rain.RainIntensity += delta;
            if (rain.RainIntensity >= 0.41f)
            {
                rain.RainIntensity = 0.41f;
                return;
            }

            await Task.Delay(50);
        }
    }

    internal async void StopToSoundRain()
    {
        if(rain.RainIntensity <= 0.1) { return; }
        while (true)
        {
            rain.RainIntensity -= delta;
            if (rain.RainIntensity <= 0.1f)
            {
                rain.RainIntensity = 0.1f;
                return;
            }

            await Task.Delay(50);
        }
    }

    
}
