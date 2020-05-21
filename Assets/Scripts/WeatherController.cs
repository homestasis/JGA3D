using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherController : SingletonMonoBehaviour<WeatherController>
{
    private SunLightController sunLight;
    private Rain3DController rain;
    private List<EnemyGardian> gardian;
    private TalkMobController[] talkMob;

    private bool isNormalRainy;
    private bool isHeavyRainy;

    protected override void Awake()
    {
        base.Awake();

        sunLight = SunLightController.Instance;
        rain = Rain3DController.Instance;
        gardian = new List<EnemyGardian>();
        GameObject[] gardOb = GameObject.FindGameObjectsWithTag("Gardian");
        foreach (GameObject g in gardOb)
        {
            gardian.Add(g.GetComponent<EnemyGardian>());
        }

        //stage2だったら的な
        GameObject farmers = GameObject.FindWithTag("Farmers");
        talkMob = new TalkMobController[farmers.transform.childCount];
        talkMob = farmers.GetComponentsInChildren<TalkMobController>();
       
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

        if (!(talkMob is null))
        {
            foreach (TalkMobController t in talkMob)
            {
                t.MobGo();
            }
        }
    }

    internal void BeSunny()
    {
        isNormalRainy = true;
        isHeavyRainy = false;
        StartCoroutine(sunLight.Lighten());
        StartCoroutine(rain.StopToSoundStrong());
        foreach (EnemyGardian g in gardian)
        {
            g.ComeBack();
        }
        if (!(talkMob is null))
        {
            foreach (TalkMobController t in talkMob)
            {
                t.MobBack();
            }
        }
    }
}
