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

    // Start is called before the first frame update
    private void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = 0.7f;
        ex = 0.6f;
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

            if (inte <= 0.40 || ex <= 0.23f)
            {
                light.intensity = 0.40f;

                ex = 0.23f;
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
            w.IncreaseWater();
            print("increase water");
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

            if (inte >= 0.7f || ex >= 0.6f)
            {
                light.intensity = 0.7f;

                ex = 0.6f;
                sky.SetFloat("_Exposure", ex);

                break;
            }
            await Task.Delay(50);
        }

        foreach (WaterController w in water)
        {
            w.DecreaseWater();
        }
    }

}
