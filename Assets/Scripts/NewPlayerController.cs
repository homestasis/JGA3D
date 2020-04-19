using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;
    public float jumpLimitTime;
    public float gravity;
    public GroundCheck ground;
    public AnimationCurve jumpCurve;

    private Rigidbody rb = null;
    private Animator anim = null;
    private CharacterController controller;
    private float jumpPos = 0.0f;
    private float jumpTime;
    private bool isJump = false;
    private bool isGround = false;

    void Start()
    {
        //コンポーネントのインスタンスを捕まえる
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //各種座標軸の速度を求める
        float xSpeed = 0.0f;
        float ySpeed = 0.0f;

        xSpeed = GetXSpeed();
        ySpeed = GetYSpeed();


        Vector3 direction = new Vector3(xSpeed, ySpeed, 0);
        controller.Move(direction * Time.deltaTime);
    }

    private float GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        float xSpeed = 0.0f;
        if (horizontalKey > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            xSpeed = -speed;
        }
        else
        {
            xSpeed = 0f;
        }
        return xSpeed;
    }

    private float GetYSpeed()
    {
        float verticalKey = Input.GetAxis("Vertical");
        float ySpeed = -gravity;
        if (verticalKey > 0 && jumpTime < jumpLimitTime)
        {
            ySpeed = jumpSpeed;
            jumpPos = transform.position.y; //ジャンプした位置を記録
            isJump = true;
            jumpTime = 0.0f;
        }
        else
        {
            isJump = false;
        }

        if (isJump)
        {
            //上ボタンを押されている。かつ、現在の高さがジャンプした位置から自分の決めた位置より下ならジャンプを継続する
            if (verticalKey > 0 && jumpPos + jumpHeight > transform.position.y && jumpTime < jumpLimitTime)
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
        return ySpeed;
    }
        
}