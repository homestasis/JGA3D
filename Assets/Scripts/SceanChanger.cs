using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanChanger : MonoBehaviour
{
    private string playerTag = "Player";
    public int nextStageNum = 3;
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
        StartCoroutine(fade.FadeOut());
        GManager.instance.stageNum = nextStageNum;
        yield return new WaitForSeconds(3f);
        GManager.instance.continueNum = 0;
        SceneManager.LoadScene("Stage" + nextStageNum);
        nextStageNum = GManager.instance.stageNum + 1;
    }
}
