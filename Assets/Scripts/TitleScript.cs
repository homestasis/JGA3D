using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    //public FadeImage fade;
    private Camera3DController cam;
    private GameObject player;
    public GameObject tempUI;//なぜかインスペクターで取らないと動かない
    public GameObject rainSwitch;//同上
    private NewPlayerController p;
    private FadeImage title;

    private bool firstPush = false;
    private float playerX;

    private void Awake()
    {
        GameObject cameraOb = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject titleLogo = GameObject.Find("TitleLogo");
        //tempUI = GameObject.Find("TempUI");
        //rainSwitch = GameObject.Find("RainSwicth");
        title = titleLogo.GetComponent<FadeImage>();
        player = GameObject.FindGameObjectWithTag("Player");
        p = player.GetComponent<NewPlayerController>();
        cam = cameraOb.GetComponent<Camera3DController>();
        playerX = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!title.compFadeOut)
        {
            title.StartFadeOut();
        }
        if (!firstPush && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(cam.PlayStart(playerX));
            firstPush = true;
        }
        if (firstPush)
        {
            StartCoroutine(TitleDestroy());
        }
    }

    private IEnumerator TitleDestroy()
    {
        title.StartFadeIn();
        yield return new WaitForSeconds(3f);
        tempUI.SetActive(true);
        rainSwitch.SetActive(true);
        p.enabled = true;
        this.enabled = false;
    }

}