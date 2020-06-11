using UnityEngine;
using System.Collections;



public class BGMSounder : SingletonMonoBehaviour<BGMSounder>
{
    private AudioSource bgm;

    protected override void Awake()
    {
        base.Awake();
        bgm = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        SetVolume(0.4f);
    }

    internal void SetVolume(float vol)
    {
        bgm.volume = vol;
    }

    internal IEnumerator FadeIn()
    {
        float val = 0.4f;
        while (true)
        {
            val -= 0.005f;
            SetVolume(val);
            if(val <= 0.03)
            {
                SetVolume(0.03f);
                yield break;
            }
            yield return null;
        }
    }

    internal IEnumerator FadeInComp()
    {
        float val = 0.03f;
        while (true)
        {
            val -= 0.003f;
            SetVolume(val);
            if (val <= 0)
            {
                SetVolume(0);
                yield break;
            }
            yield return null;
        }
    }

    internal void initiate()
    {
        bgm.time = 0f;
        SetVolume(0.4f);
    }

}
