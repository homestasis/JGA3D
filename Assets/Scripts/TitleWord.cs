using System.Collections;
using UnityEngine;


public class TitleWord : FadeImage
{
    private bool isFlash;
    

    private void Start()
    {
        compFadeIn = true;
        isFlash = true;
        fadeSpeed = 0.0072f;
    }

    private void Update()
    {
        if (!isFlash) { return; }

        if(compFadeIn)
        {
            FadeOut();
        }
        else if(compFadeOut)
        {
            FadeIn();
        }
    }

    internal void Stop()
    {
        isFlash = false;
        fadeIm.enabled = false;
    }

    private new void FadeIn()
    {
        if(!compFadeIn)
        {
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
            if (alfa <= 0)
            {                    //c)完全に透明になったら処理を抜ける
                compFadeIn = true;
                fadeIm.enabled = false;//d)パネルの表示をオフにする
                compFadeOut = false;
            }
        }
        
    }

    private new void FadeOut()
    {
        fadeIm.enabled = true;  // a)パネルの表示をオンにする
        if(!compFadeOut)
        {
            alfa += fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
            if (alfa >= 1)
            {             // d)完全に不透明になったら処理を抜ける
                compFadeOut = true;
                compFadeIn = false;
            }

        }
        
    }
}
