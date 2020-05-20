using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherController : SingletonMonoBehaviour<WeatherController>
{
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

    protected override void Awake()
    {
        sunLight = SunLightController.Instance;
        rain = Rain3DController.Instance;
        gardian = new List<EnemyGardian>();
        GameObject[] gardOb = GameObject.FindGameObjectsWithTag("Gardian");
        foreach (GameObject g in gardOb)
        {
            gardian.Add(g.GetComponent<EnemyGardian>());
        }
     /*   talkMob = new List<TalkMobController>();
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
        */
        isNormalRainy = true;
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
        StartCoroutine(rain.StopToSoundStrong());
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
