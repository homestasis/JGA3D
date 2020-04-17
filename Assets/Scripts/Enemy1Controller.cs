using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    [Header("動く範囲")]
    public float turningPointMin;
    public float turningPointMax;
    [Header("Y座標")]
    public float pointY;
    [Header("最小の歩く距離")]
    public float minWalkDist;
    [Header("フレームごとに動く距離")]
    public float disPerFrame;

    private bool isRight;
    private bool isLeft;
    private bool isFront;

    private float turningPoint;

    private PlayerController playerScript;
    private System.Random rand;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        rand = new System.Random();
        anim = GetComponent<Animator>();

        transform.position = new Vector3(turningPointMin, pointY, 0);
        turningPoint = turningPointMax;
        StartToWalkRight();
    }

    void Update()
    {
        if (isRight)
        {
            transform.Translate(disPerFrame, 0, 0);
        }
        else if (isLeft)
        {
            transform.Translate(-1 * disPerFrame, 0, 0);
        }
        else if (isFront)
        {
           // playerScript.SeenByEnemy();
        }

        if(!isFront && (System.Math.Abs(transform.position.x - turningPoint) <= disPerFrame/2))
        {
            isRight = false;
            isLeft = false;
            LookFront();
        }

        SetAnimation();
    }

    public void StartToWalk()
    {
        isFront = false;

        if(transform.position.x > (turningPointMin + turningPointMax)/2)
        {
            StartToWalkLeft();
        }
        else
        {
            StartToWalkRight();
        }
    }

    private void StartToWalkRight()
    {
        isRight = true;

        int min = (int)(10 * (transform.position.x + minWalkDist));
        int max = (int)(10 * turningPointMax) + 1;
        if(min > max) { min = max; }

        int n = rand.Next(min, max);
        turningPoint = n / 10;
    }

    private void StartToWalkLeft()
    {
        isLeft = true;

        int max = (int)(10 * (transform.position.x - minWalkDist)) + 1;
        int min = (int)(10 * turningPointMin);
        if(max < min) { max = min;}

        int n = rand.Next(min, max);
        turningPoint = n / 10;
    }
    

    private void LookFront()
    {
        isFront = true;
        //playerScript.SeenByEnemy();
    }

    private void SetAnimation()
    {
        anim.SetBool("IsWalkRight", isRight);
        anim.SetBool("IsWalkLeft", isLeft);
        anim.SetBool("IsFront", isFront);
    }

    
}
