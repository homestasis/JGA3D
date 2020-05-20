using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SunLightController : SingletonMonoBehaviour<SunLightController>
{
    [SerializeField]private Material sky;
    [SerializeField] private float delta;

    private Light sunLight;
    private WeatherController weather;

    private List<WaterController> water;
    private List<GrassController> grass;

    private float ex;
    private const float minLight = 0.2f;
    private const float maxLight = 0.9f;
    private const float minSun = 0.3f;
    private const float maxSun = 1f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        sunLight = GetComponent<Light>();
        sunLight.intensity = maxLight;
        ex = maxSun;
        sky.SetFloat("_Exposure", ex);

        weather = WeatherController.Instance;

        water = new List<WaterController>();
        GameObject[] waterOb = GameObject.FindGameObjectsWithTag("Water");
        foreach(GameObject w in waterOb)
        {
            water.Add(w.GetComponent<WaterController>());
        }
        

        grass = new List<GrassController>();
        GameObject[] grassOb = GameObject.FindGameObjectsWithTag("Grass");
        foreach(GameObject g in grassOb)
        {
            grass.Add(g.GetComponent<GrassController>());
        }

    }

    internal IEnumerator Darken()
    { 
        while (true)
        {
            float inte = sunLight.intensity;
            sunLight.intensity -= delta;

            ex -= delta;
            sky.SetFloat("_Exposure", ex);

            if (inte <= minLight || ex <= minSun)
            {
                sunLight.intensity = minLight;

                ex = minSun;
                sky.SetFloat("_Exposure", ex);


                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        foreach(GrassController g in grass)
        {
            g.GrowGrass();
        }
       
        foreach(WaterController w in water)
        {
            StartCoroutine(w.IncreaseWater());
           // Debug.Log("start coroutine");
        }

    }

    internal IEnumerator Lighten()
    {
        while(true)
        {

            float inte = sunLight.intensity;
            sunLight.intensity += delta;

            ex += delta;
            sky.SetFloat("_Exposure", ex);

            if (inte >= maxLight || ex >= maxSun)
            {
                sunLight.intensity = maxLight;

                ex = maxSun;
                sky.SetFloat("_Exposure", ex);

                break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        foreach (WaterController w in water)
        {
            StartCoroutine(w.DecreaseWater());
        }
    }

    internal void EnterDarkPlace()
    {
        sunLight.intensity = 0.01f;
    }

    internal void ExitDarkPlace()
    {
        if (weather.GetIsNormalRainy())
        {
            sunLight.intensity = maxLight;
            Debug.Log("max light");
        }
        else
        {
            sunLight.intensity = minLight;
            Debug.Log("min light");
        }
        

    }

}
