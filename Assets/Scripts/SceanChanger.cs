using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanChanger : MonoBehaviour
{
    private string playerTag = "Player";
    private int nextStageNum = 2;
    private GameObject black;
    private FadeImage fade;

    private void Start()
    {
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag)
        {
            StartCoroutine(SceanChange());
        }
    }

    private IEnumerator SceanChange()
    {
        StartCoroutine(fade.StartFadeOut());
        yield return new WaitForSeconds(3f);
        GManager.instance.stageNum = nextStageNum;
        GManager.instance.continueNum = 0;
        SceneManager.LoadScene("Stage" + nextStageNum);
    }
}
