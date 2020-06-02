using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Start : MonoBehaviour
{
    private string playerTag = "Player";
    private GameObject black;
    private FadeImage fade;
    private RainSwitcher rain;
    private TempUiController temp;


    private void Start()
    {
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
        rain = RainSwitcher.Instance;
        temp = TempUiController.Instance;
        Starting();
    }

    private void Starting()
    {
        StartCoroutine(fade.FadeIn());
        rain.enabled = true;
        temp.enabled = true;
    }

}
