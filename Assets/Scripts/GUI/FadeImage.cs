using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class FadeImage : MonoBehaviour
{
    public float fadeSpeed = 0.008f;
    protected bool compFadeIn;
	protected bool compFadeOut;


    protected Image fadeIm;
    protected float red,green,blue,alfa;

    private void Awake()
    {
        fadeIm = GetComponent<Image>();
        red = fadeIm.color.r;
        green = fadeIm.color.g;
        blue = fadeIm.color.b;
        alfa = fadeIm.color.a;
    }
   

    internal IEnumerator FadeIn()
    {
        while (!compFadeIn)
        {
            alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
            SetAlpha();                      //b)変更した不透明度パネルに反映する
            if (alfa <= 0)
            {                    //c)完全に透明になったら処理を抜ける
                compFadeIn = true;
                fadeIm.enabled = false;//d)パネルの表示をオフにする
            }

            yield return null;
        }
		compFadeOut = false;
    }

    internal IEnumerator FadeOut()
    {
        fadeIm.enabled = true;  // a)パネルの表示をオンにする
        while (!compFadeOut)
        {
            alfa += fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
            if (alfa >= 1)
            {             // d)完全に不透明になったら処理を抜ける
                compFadeOut = true;
            }

            yield return null;
        }
		compFadeIn = false;
    }

    protected void SetAlpha()
    {
        fadeIm.color = new Color(red, green, blue, alfa);
    }

    public bool CompFadeOut()
    {
        if (compFadeOut)
        {
            return true;
        }
        return false;
    }
}
