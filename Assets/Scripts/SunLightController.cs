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



    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = 1f;
        sky.SetFloat("_Exposure", 1f);
        ex = 1f;
    }

    public async void Darken()
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


    }


   
    public  void LightLighten()
    {
       while(true)
        {
            float inte = light.intensity;
            light.intensity += delta;
            if (inte >= 1f)
            {
                break;
            }
            Thread.Sleep(50);
        }

    }

   

    public void SkyLighten()
    {
        while(true)
        {
            ex += delta;
            sky.SetFloat("_Exposure", ex);
            if (ex >= 1f)
            {
                ex = 1f;
                sky.SetFloat("_Exposure", ex);
                break;
            }
            Thread.Sleep(50);
        }
    }


    
}
