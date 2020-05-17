using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class WeatherController : MonoBehaviour
{
    [SerializeField] private GameObject sunLightOb;
    [SerializeField] private GameObject rainPf;

    private SunLightController sunLight;
    private Rain3DController rain;
    private List<EnemyGardian> gardian;
    private List<TalkMobController> talkMob;
    private GameObject talkmob1;
    private GameObject talkmob2;
    private GameObject talkmob3;
    private GameObject talkmob4;
    private GameObject talkmob5;

    private bool isNormalRainy;
    private bool isHeavyRainy;

    private void Awake()
    {
        sunLight = sunLightOb.GetComponent<SunLightController>();
        rain = rainPf.GetComponent<Rain3DController>();
        isNormalRainy = true;
        gardian = new List<EnemyGardian>();
        GameObject[] gardOb = GameObject.FindGameObjectsWithTag("Gardian");
        foreach (GameObject g in gardOb)
        {
            gardian.Add(g.GetComponent<EnemyGardian>());
        }
        talkMob = new List<TalkMobController>();
        talkmob1 = GameObject.Find("Murabito1");
        talkMob.Add(talkmob1.GetComponent<TalkMobController>());
        talkmob2 = GameObject.Find("Murabito1");
        talkMob.Add(talkmob2.GetComponent<TalkMobController>());
        talkmob3 = GameObject.Find("Murabito1");
        talkMob.Add(talkmob3.GetComponent<TalkMobController>());
        talkmob4 = GameObject.Find("Murabito1");
        talkMob.Add(talkmob4.GetComponent<TalkMobController>());
        talkmob5 = GameObject.Find("Murabito1");
        talkMob.Add(talkmob5.GetComponent<TalkMobController>());
    }

    internal bool GetIsNormalRainy() { return isNormalRainy; }
    internal bool GetIsHeavyRainy() { return isHeavyRainy;  }

    internal async Task BeRainnyAsync()
    {
        isNormalRainy = false;
        isHeavyRainy = true;
        sunLight.Darken();
        await Task.Delay(1200);
        rain.StartToSoundRain();
        foreach (EnemyGardian g in gardian)
        {
            g.RunAway();
        }
        foreach (TalkMobController t in talkMob)
        {
            t.MobGo();
        }
    }

    internal void BeSunny()
    {
        isNormalRainy = true;
        isHeavyRainy = false;
        sunLight.Lighten();
        rain.StopToSoundRain();
        foreach (EnemyGardian g in gardian)
        {
            g.ComeBack();
        }
        foreach (TalkMobController t in talkMob)
        {
            t.MobBack();
        }
    }
}
