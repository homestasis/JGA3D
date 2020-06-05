using System.Collections;
using UnityEngine;


public class WeatherController : SingletonMonoBehaviour<WeatherController>
{
    private SunLightController sunLight;
    private Rain3DController rain;
    private ShoppingFarmer[] talkMob;

    private bool isNormalRainy;
    private bool isHeavyRainy;

    protected override void Awake()
    {
        base.Awake();

        sunLight = SunLightController.Instance;
        rain = Rain3DController.Instance;
        isNormalRainy = true;
    }


    internal void initiate()
    {
        GameObject farmers = GameObject.FindWithTag("Farmers");
        talkMob = new ShoppingFarmer[farmers.transform.childCount];
        talkMob = farmers.GetComponentsInChildren<ShoppingFarmer>();
    }

   
    internal bool GetIsNormalRainy() { return isNormalRainy; }
    internal bool GetIsHeavyRainy() { return isHeavyRainy;  }

    internal IEnumerator BeRainnyAsync()
    {
        isNormalRainy = false;
        isHeavyRainy = true;
        StartCoroutine(sunLight.Darken());
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(rain.StartToSoundStrong());

        if (!(talkMob is null))
        {
            foreach (ShoppingFarmer t in talkMob)
            {
                StartCoroutine(t.Amayadori());
            }
        }
    }

    internal void BeSunny()
    {
        isNormalRainy = true;
        isHeavyRainy = false;
        StartCoroutine(sunLight.Lighten());
        StartCoroutine(rain.StopToSoundStrong());
        if (!(talkMob is null))
        {
            foreach (ShoppingFarmer t in talkMob)
            {
                StartCoroutine(t.UnAmayadori());
            }
        }
    }
}
