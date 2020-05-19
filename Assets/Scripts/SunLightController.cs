using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class SunLightController : MonoBehaviour
{
    [SerializeField]private Material sky;
    [SerializeField] private float delta;

    private new Light light;
    private float ex;

    private List<WaterController> water;
    private List<GrassController> grass;
    // private FireController fire;

    private float minLight;
    private float maxLight;

    private float minSun;
    private float maxSun;

    // Start is called before the first frame update
    private void Awake()
    {
        minLight = 0.2f;
        maxLight = 0.9f;

        minSun = 0.3f;
        maxSun = 1f;

        light = GetComponent<Light>();
        light.intensity = maxLight;
        ex = maxSun;
        sky.SetFloat("_Exposure", ex);
        

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

        /*
        GameObject fireOb = GameObject.Find("fireplace");
        fire = fireOb.GetComponent<FireController>();
        */

    }

    internal async void Darken()
    { 
        while (true)
        {
            float inte = light.intensity;
            light.intensity -= delta;

            ex -= delta;
            sky.SetFloat("_Exposure", ex);

            if (inte <= minLight || ex <= minSun)
            {
                light.intensity = minLight;

                ex = minSun;
                sky.SetFloat("_Exposure", ex);


                break;
            }
            await Task.Delay(50);
        }

        foreach(GrassController g in grass)
        {
            g.GrowGrass();
        }
        foreach(WaterController w in water)
        {
            StartCoroutine(w.IncreaseWater());
        }

    //    fire.PutOutFire();

    }

    internal async void Lighten()
    {
        while(true)
        {

            float inte = light.intensity;
            light.intensity += delta;

            ex += delta;
            sky.SetFloat("_Exposure", ex);

            if (inte >= maxLight || ex >= maxSun)
            {
                light.intensity = maxLight;

                ex = maxSun;
                sky.SetFloat("_Exposure", ex);

                break;
            }
            await Task.Delay(50);
        }

        foreach (WaterController w in water)
        {
            StartCoroutine(w.DecreaseWater());
        }
    }

}
