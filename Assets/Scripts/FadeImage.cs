using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public float fadeSpeed = 0.01f;
    public bool compFadeIn = false;
    public bool compFadeOut = false;
    public bool isFadeIn = false;
    public bool isFadeOut = false;

    Image fadeImage;
    float red,green,blue,alfa;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }
   

    public IEnumerator StartFadeIn()
    {
        while (!compFadeIn)
        {
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
            if (alfa <= 0)
            {                    //c)完全に透明になったら処理を抜ける
                isFadeIn = false;
                compFadeIn = true;
                fadeImage.enabled = false;//d)パネルの表示をオフにする
            }

            yield return null;
        }
    }

    public IEnumerator StartFadeOut()
    {
        fadeImage.enabled = true;  // a)パネルの表示をオンにする
        while (!compFadeOut)
        {
            alfa += fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
            if (alfa >= 1)
            {             // d)完全に不透明になったら処理を抜ける
                isFadeOut = false;
                compFadeOut = true;
            }

            yield return null;
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
