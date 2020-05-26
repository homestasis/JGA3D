﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    //public FadeImage fade;
    private Camera3DController cam;
    private Canvas rainCanvas;
    private Canvas tempCanvas;
    private NewPlayerController p;
    private FadeImage title;

    private bool firstPush;
    private float playerX;

    private void Awake()
    {
        GameObject cameraOb = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject titleLogo = transform.GetChild(0).gameObject;
        GameObject tempUI = GameObject.Find("TempUI");
        GameObject rainSwitch = GameObject.Find("RainSwitch");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        title = titleLogo.GetComponent<FadeImage>();
        tempCanvas = tempUI.GetComponent<Canvas>();
        rainCanvas = rainSwitch.GetComponent<Canvas>();
        p = player.GetComponent<NewPlayerController>();
        cam = cameraOb.GetComponent<Camera3DController>();

        playerX = player.transform.position.x;
    }

    private void Start()
    {
        rainCanvas.enabled = false;
        tempCanvas.enabled = false;

        StartCoroutine(title.StartFadeOut());
    }

    // Update is called once per frame
    void Update()
    {

        if(firstPush)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(TitleDestroy());
            firstPush = true;
        }
       
    }

    private IEnumerator TitleDestroy()
    {
        StartCoroutine(title.StartFadeIn());
        yield return StartCoroutine(cam.PlayStart(playerX));

        rainCanvas.enabled = true;
        tempCanvas.enabled = true;
        
        p.enabled = true;  
        Destroy(gameObject);
    }

}