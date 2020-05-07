﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;

public partial class NewPlayerController : MonoBehaviour
{
    public float speed;
    public float gravity;
    public float jumpSpeed;
    public float jumpHeight;
    public float jumpLimitTime;
    public float climbSpeed;
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;
    public LadderCheck ladderChecker;

    [SerializeField] private SunLightController sunLight;
    private Rigidbody rb = null;
    private CharacterController controller = null;
    private Animator anim = null;
    private bool isRun = false;
    private bool isJump = false;
    private bool isLadder = false;
    private bool rainKey = false;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private float beforeKey;

    private bool fall;
    private float surfaceP;
    private bool inWater;

    [SerializeField] private float nomalTempDecrease;
    [SerializeField] private float heavyTempDecrease;

    [SerializeField]
    private GameObject tempUI;
    private Slider tempSlider;

    [SerializeField] private float speedPropInHeavyRain;

    private float xSpeed;
    private float ySpeed;
    private float xSpeedBefore;

    private AudioSource audio;

    private List<SpeechChange> speechScripts;
    private GameObject[] farmers;
    private List<FarmerController> farmerScripts;

    [SerializeField] private GameObject cameraOb;
    private Camera3DController cam;



    private void Awake()
    {
        speechScripts = new List<SpeechChange>();
        farmerScripts = new List<FarmerController>();

        farmers = GameObject.FindGameObjectsWithTag("Farmer");
        foreach(GameObject g in farmers)
        {
            GameObject speechBubble = g.transform.Find("SpeechBubble").gameObject;
            speechScripts.Add(speechBubble.GetComponent<SpeechChange>());
            farmerScripts.Add(g.GetComponent<FarmerController>());
        }
        
        cam = cameraOb.GetComponent<Camera3DController>();
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        weather = weatherOb.GetComponent<WeatherController>();

        tempSlider = tempUI.transform.Find("TempBar").GetComponent<Slider>();
        tempSlider.value = 1f;

        SetNormalRain();

        nomalTempDecrease = nomalTempDecrease * Time.deltaTime;
        heavyTempDecrease = heavyTempDecrease * Time.deltaTime;

        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetRain();
        DecreaseTempreture();
        if (tempSlider.value <= 0)
        {
            //GameOver
        }

        Conversation();
        if (isStop)
        {
            transform.position = StopPoint;
            return;
        }
        isLadder = ladderChecker.IsLadder();
        GetXSpeed();
        xSpeedBefore = xSpeed;
        if (!inWater)
        {
            GetYSpeed();
        }
        if (inWater)
        {
            GetYSPeedInWater();
        }
        if (rainKey)
        {
            xSpeed = xSpeed * speedPropInHeavyRain;
            ySpeed = ySpeed * speedPropInHeavyRain;
        }

        Vector3 direction = new Vector3(xSpeed, ySpeed, 0);
        controller.Move(direction * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        SetAnimation();
    }

    internal void SetNormalRain()
    {
        rainKey = false;
    }

    internal void SetHeavyRain()
    {
        rainKey = true;
    }

    internal void GetIntoWater(float pos)
    {
        inWater = true;
        fall = true;
        surfaceP = pos;
    }

    internal void setSurfaceP(float pos)
    {
        surfaceP = pos;
    }


    internal void ResetWater()
    {
        inWater = false;
        fall = false;
    }

    private void DecreaseTempreture()
    {
        if (rainKey)
        {
            tempSlider.value -= heavyTempDecrease;
        }
        else
        {
            tempSlider.value -= nomalTempDecrease;
        }
    }

    internal void RecoveryTemp(float plusTemp)
    {
        tempSlider.value += plusTemp;
    }

    private void GetXSpeed()
    {
        if (!controller.isGrounded && !isLadder)
        {
            xSpeed = xSpeedBefore;
            return;
        }

        float horizontalKey = Input.GetAxis("Horizontal");
        xSpeed = 0f;
        if (horizontalKey > 0)
        {
            if (isLadder)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            dashTime += Time.deltaTime;
            isRun = true;
            xSpeed = speed;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else if (horizontalKey < 0)
        {
            if (isLadder)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
            dashTime += Time.deltaTime;
            isRun = true;
            xSpeed = -speed;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
            audio.Stop();
        }
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;
　       //アニメーションカーブを速度に適用 New
        xSpeed *= dashCurve.Evaluate(dashTime);
    }

    private void GetYSpeed()
    {
        //float verticalKey = Input.GetAxis("Vertical");
        bool jump = Input.GetKey(KeyCode.Space);
        ySpeed = -gravity;
        if (isLadder)
        {
            if (jump)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                ySpeed = climbSpeed;
                isRun = true;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                ySpeed = -climbSpeed;
                isRun = true;
            }

        }
        if (controller.isGrounded)
        {
            if (jump && jumpTime < jumpLimitTime)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する
                isJump = true;
                jumpTime = 0.0f;
                audio.Stop();
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上ボタンを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if (jump && jumpPos + jumpHeight > transform.position.y && jumpTime < jumpLimitTime)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

    }


    private void GetYSPeedInWater()
    {
        bool pushSpace = Input.GetKey(KeyCode.Space);
        ySpeed = -gravity;
        if (!isJump)
        {
            if (pushSpace && jumpTime < jumpLimitTime)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する
                isJump = true;
                jumpTime = 0.0f;
                fall = true;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上ボタンを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if (pushSpace && jumpPos + jumpHeight > transform.position.y && jumpTime < jumpLimitTime)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }

        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        else
        {
            float y = transform.position.y;
            float gap = surfaceP - y;
            if (gap >= 0.2480) { fall = false; }

            float tem = CalculateBouyancy(y);
            float bouyanValue = fall ? (float)(tem * 0.40) : (float)(tem * 0.6);
            ySpeed += bouyanValue;
        }
    }
    private float CalculateBouyancy(float posY)
    {
        return (float)(gravity * (surfaceP - posY) / 0.10);
    }

    private void GetRain()
    {
        if (!rainKey && Input.GetKeyDown(KeyCode.Return))
        {
            weather.BeRainnyAsync();
            rainKey = true;
        }
        else if (rainKey && Input.GetKeyDown(KeyCode.Return))
        {
            weather.BeSunny();
            rainKey = false;
        }
    }

    private void Conversation()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            for(int i =0; i<speechScripts.Count; i++)
            {
                if(speechScripts[i].GetIsDisplay())
                {
                    StartCoroutine(StartToConversation(i));
                }
            }
        }
    }

    private IEnumerator StartToConversation(int i)
    {
        speechScripts[i].ResetIsDisplay();
        StopPlayer();
        Vector3 pos = farmers[i].transform.position;
        //Farmerの方をみる
        transform.LookAt(pos);
        farmerScripts[i].LookToPlayer();
        //カメラのzoom
        cam.ZoomIn(pos);
        //会話開始
        yield return StartCoroutine(farmerScripts[i].Talk());

        //会話終了後
        cam.ZoomAuto();
        farmerScripts[i].ResetDirection();
        ResetIsStop();
    }

    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("run", isRun);
    }
}
