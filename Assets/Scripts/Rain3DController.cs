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
        rain.RainIntensity = 0.4f;
    }

    internal async void StartToSoundRain()
    {
        if (rain.RainIntensity >= 0.7f) { return; }
        while (true)
        {
            rain.RainIntensity += delta;
            if (rain.RainIntensity >= 0.7f)
            {
                rain.RainIntensity = 0.7f;
                return;
            }

            await Task.Delay(50);
        }
    }

    internal async void StopToSoundRain()
    {
        if(rain.RainIntensity <= 0.4) { return; }
        while (true)
        {
            rain.RainIntensity -= delta;
            if (rain.RainIntensity <= 0.4f)
            {
                rain.RainIntensity = 0.4f;
                return;
            }

            await Task.Delay(50);
        }
    }

    
}
