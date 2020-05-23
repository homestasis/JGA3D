using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public FadeImage fade;

    private bool firstPush = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PressStart();
        }
        if (fade.compFadeOut)
        {
            SceneManager.LoadScene("FirstStage");
        }
    }

    public void PressStart()
    {
        if (!firstPush)
        {
            fade.isFadeOut = true;
            Debug.Log("start");
            fade.StartFadeOut();
            firstPush = true;
        }
    }
}
