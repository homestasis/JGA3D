using System.Collections;
using UnityEngine;


public class TitleWord : FadeImage
{
    private bool isFlash;

    internal IEnumerator Flash()
    {
        isFlash = true;
        while (isFlash)
        {
            StartCoroutine(StartFadeIn());
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(StartFadeOut());
            yield return new WaitForSeconds(0.8f);

        }
    }

    internal void Stop()
    {
        Destroy(gameObject);
    }
}
