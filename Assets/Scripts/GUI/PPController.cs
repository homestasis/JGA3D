using System.Collections;
using UnityEngine.Rendering.PostProcessing;



public class PPController : SingletonMonoBehaviour<PPController>
{

    private PostProcessVolume ppVolume;
    private Vignette vig;
    private bool isMidFlash;
    private bool isHeavyFlash;
    private float low;
    private float high;
    private BGMSounder bgm;

    protected override void Awake()
    {
        base.Awake();
        ppVolume = GetComponent<PostProcessVolume>();
        foreach (PostProcessEffectSettings item in ppVolume.profile.settings)
        {
            if (item as Vignette)
            {
                vig = item as Vignette;
            }
        }
        bgm = BGMSounder.Instance;
    }

    private void Start()
    {
        ppVolume.enabled = false;
    }

    internal void SetFalse()
    {
        isMidFlash = false;
        isHeavyFlash = false;
    }

    internal void TurnOnMidEffect()
    {
        if (isMidFlash) { return; }

        StartCoroutine(bgm.FadeInComp());

        ppVolume.enabled = true;
        isMidFlash = true;

        low = 0.1f;
        high = 0.45f;

        vig.intensity.value = low;
        StartCoroutine(Flashing());
    }

    internal void TurnOnHeavyEffect()
    {
        if (isHeavyFlash) { return; }

        isHeavyFlash = true;

        low = 0.45f;
        high = 0.75f;

        vig.intensity.value = low;
    }

    internal void TurnOffEffect()
    {
        isMidFlash = false;
        isHeavyFlash = false;
    }

    private IEnumerator Flashing()
    {
        bool beHigh = true;

        while (isMidFlash)
        {
            if (beHigh)
            {
                vig.intensity.value += 0.01f;
            }
            else
            {
                vig.intensity.value -= 0.01f;
            }

            if(vig.intensity.value >= high)
            {
                vig.intensity.value = high;
                beHigh = false;
            }
            if (vig.intensity.value <= low)
            {
                vig.intensity.value = low;
                beHigh = true;
            }

            yield return null;
        }
    }
}
