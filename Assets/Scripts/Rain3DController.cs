using System.Collections;
using UnityEngine;
using DigitalRuby.RainMaker;

public class Rain3DController : SingletonMonoBehaviour<Rain3DController>
{

    [SerializeField] private float delta;
    private RainScript rain;
    private float minRainInt;
    private float maxRainInt;
    private bool isStrongSound;
    
    protected override void Awake()
    {
        base.Awake();
        rain = this.GetComponent<RainScript>();
        rain.RainIntensity = 0.1f;
    }

    private void Start()
    {
        minRainInt = 0.1f;
        maxRainInt = 0.4f;

    }
    internal IEnumerator StartToSoundStrong()
    {
        if (rain.RainIntensity >= maxRainInt) { isStrongSound = true; }
        while (!isStrongSound)
        {
            rain.RainIntensity += delta;
            if (rain.RainIntensity >= maxRainInt)
            {
                rain.RainIntensity = maxRainInt;
                isStrongSound = true;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    internal IEnumerator StopToSoundStrong()
    {
        if(rain.RainIntensity <= minRainInt) { isStrongSound = false; }
        while (isStrongSound)
        {
            rain.RainIntensity -= delta;
            if (rain.RainIntensity <= minRainInt)
            {
                rain.RainIntensity = minRainInt;
                isStrongSound = false;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    
}
