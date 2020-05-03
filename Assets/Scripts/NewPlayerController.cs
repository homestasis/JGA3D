﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //クワバラが追加した.
    private bool isInWater;
  //  private bool goToUpper;

    [Header("何フレームで1周期か")]
    [SerializeField] private float tSeconds;
    private float tFrame;
    [Header("振幅")]
    [SerializeField] private float amp;

    private float surfaceP;
    private float dir;

    private bool fall;

  //  [Header("浮力")]

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        weather = weatherOb.GetComponent<WeatherController>();
        tFrame = tSeconds / Time.deltaTime;     
    }

    private void Update()
    {
        if(isStop)
        {
            transform.position = StopPoint;
            return;
        }

        isLadder = ladderChecker.IsLadder();

        float xSpeed = 0.0f;
        float ySpeed = 0.0f;

        xSpeed = GetXSpeed();

        if (!isInWater) { ySpeed = GetYSpeed(); }
        else
        {
            int i = xSpeed < 0 ? -1 : 1;
            xSpeed = (float)(i * Mathf.Abs(xSpeed) * 0.8);

            ySpeed = GetYSPeedInWater();
        }

        Vector3 direction = new Vector3(xSpeed, ySpeed, 0);
        controller.Move(direction * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        GetRain();

        SetAnimation();
    }

    internal void SetIsInWater(float y)
    {
        isInWater = true;
        surfaceP = y;
        //  dir = -1;
        fall = true;
        print("water in ");

    }

    internal void ResetIsInWater()
    {
        isInWater = false;
        print("water reset");
    }

    

    private float GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        if(horizontalKey > 0)
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
        }
        else if(horizontalKey < 0)
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
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
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
        return xSpeed;
    }

    private float GetYSpeed()
    {
        bool pushSpace = Input.GetKey(KeyCode.Space);
        float ySpeed = -gravity;
        if (isLadder)
        {
            if (pushSpace)
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
            if (pushSpace && jumpTime < jumpLimitTime)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y; //ジャンプした位置を記録する
                isJump = true;
                jumpTime = 0.0f;
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
        return ySpeed;
    }

    private float GetYSPeedInWater()
    {
        bool pushSpace = Input.GetKey(KeyCode.Space);
        float ySpeed = -gravity;
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
            if (pushSpace&& jumpPos + jumpHeight > transform.position.y && jumpTime < jumpLimitTime)
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
            print("surfaceP:"+surfaceP + "y:" + y +  "gap:" + gap);
            if (gap  >= 0.2480) { fall = false; }

            float tem = CalculateBouyancy(y);
            float bouyanValue = fall ? (float)(tem * 0.40) : (float)(tem*0.6);
            ySpeed += bouyanValue;
        }
        print(fall);
        print(ySpeed);
        return ySpeed;
    }
    private float CalculateBouyancy(float posY)
    {
        return (float)(gravity*(surfaceP - posY)/0.10);
    }

    private void GetRain()
    {
        if(!rainKey && Input.GetKeyDown(KeyCode.Return))
        {
            weather.BeRainnyAsync();
            rainKey = true;
        }
        else if(rainKey && Input.GetKeyDown(KeyCode.Return))
        {
            weather.BeSunny();
            rainKey = false;
        }
    }

    private void SetAnimation()
    {
        anim.SetBool("jump",isJump);
        anim.SetBool("run", isRun);
    }
}