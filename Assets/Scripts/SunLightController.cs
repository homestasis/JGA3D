using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class SunLightController : MonoBehaviour
{
    [SerializeField]private Material sky;
    [SerializeField]private GrassController grass;
    [SerializeField] private float delta;

    private new Light light;
    private float ex;

    private List<WaterController> water;

    // Start is called before the first frame update
    private void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = 1f;
        sky.SetFloat("_Exposure", 1f);
        ex = 1f;

        water = new List<WaterController>();
        GameObject[] waterOb = GameObject.FindGameObjectsWithTag("Water");
        foreach(GameObject w in waterOb)
        {
            water.Add(w.GetComponent<WaterController>());
        }
    }

    internal async void Darken()
    {
        while (true)
        {
            float inte = light.intensity;
            light.intensity -= delta;

            ex -= delta;
            sky.SetFloat("_Exposure", ex);

            if (inte <= 0.07f || ex <= 0.01f)
            {
                light.intensity = 0.07f;

                ex = 0.01f;
                sky.SetFloat("_Exposure", ex);


                break;
            }
            await Task.Delay(50);
        }

        grass.GrowGrass();
        foreach(WaterController w in water)
        {
            w.IncreaseWater();
        }

    }

    internal async void Lighten()
    {
        while(true)
        {

            float inte = light.intensity;
            light.intensity += delta;

            ex += delta;
            sky.SetFloat("_Exposure", ex);

            if (inte >= 1f || ex >= 1f)
            {
                light.intensity = 1f;

                ex = 1f;
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
