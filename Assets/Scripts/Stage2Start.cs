using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Start : MonoBehaviour
{
    private string playerTag = "Player";
    private GameObject black;
    private FadeImage fade;

    private void Start()
    {
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            StartCoroutine(fade.FadeOut());
        }
    }
}
