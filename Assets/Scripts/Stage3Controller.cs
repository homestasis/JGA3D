using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Controller : MonoBehaviour
{
    private GameObject black;
    private FadeImage fade;
    private bool compFadeIn = false;
    // Start is called before the first frame update
    void Start()
    {
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!compFadeIn)
        {
            StartCoroutine(fade.FadeIn());
            compFadeIn = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SceanChange());
        }
    }

    private IEnumerator SceanChange()
    {
        StartCoroutine(fade.FadeOut());
        //GManager.instance.stageNum = 1;
        yield return new WaitForSeconds(3f);
        //GManager.instance.continueNum = 0;
        SceneManager.LoadScene("FirstStage");
    }
}
