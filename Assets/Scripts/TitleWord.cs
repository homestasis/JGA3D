using System.Collections;


public class TitleWord : FadeImage
{
    private bool isFlash;

   
    internal IEnumerator Flash()
    {
        isFlash = true;
        while (isFlash)
        {
            yield return StartCoroutine(StartFadeIn());
            yield return StartCoroutine(StartFadeOut());
        }
    }

    internal void Stop()
    {
        isFlash = false;
        fadeIm.enabled = false;      
    }
}
