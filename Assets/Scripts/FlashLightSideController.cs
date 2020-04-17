using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSideController : MonoBehaviour
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
    private float sumTime;

    private PlayerController playerScript;
    private GameObject frontLight;
    private FlashLightFrontController frontLightScript;
    private System.Random rand;
   



    // Start is called before the first frame update
    void Start()
    {
        frontLight = GameObject.Find("FlashLightFront");
        frontLightScript = frontLight.GetComponent<FlashLightFrontController>();
        frontLight.SetActive(false);
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        rand = new System.Random();
       

        transform.position = new Vector3(turningPointMin, pointY, 0);
        turningPoint = turningPointMax;
        StartToWalkRight();
    }

    void Update()
    {
        if (isRight)
        {
            transform.Translate(0, disPerFrame, 0);
        }
        else if (isLeft)
        {
            transform.Translate(0, disPerFrame, 0);
        }
        else if (isFront)
        {
            sumTime += Time.deltaTime;
            if(sumTime > 0.4)
            {
                StartToWalk();
            }
            // playerScript.SeenByEnemy();
        }

        if (!isFront && (System.Math.Abs(transform.position.x - turningPoint) <= disPerFrame / 2))
        {
            isRight = false;
            isLeft = false;
            LookFront();
        }

      
    }

    public void StartToWalk()
    {
        isFront = false;
        sumTime = 0;
        frontLight.SetActive(false);

        if (transform.position.x > (turningPointMin + turningPointMax) / 2)
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
        transform.rotation = Quaternion.Euler(0, 0, 270);

        int min = (int)(10 * (transform.position.x + minWalkDist));
        int max = (int)(10 * turningPointMax) + 1;
        if (min > max) { min = max; }

        int n = rand.Next(min, max);
        turningPoint = n / 10;
    }

    private void StartToWalkLeft()
    {
        isLeft = true;
        transform.rotation = Quaternion.Euler(0, 0, 90);

        int max = (int)(10 * (transform.position.x - minWalkDist)) + 1;
        int min = (int)(10 * turningPointMin);
        if (max < min) { max = min; }

        int n = rand.Next(min, max);
        turningPoint = n / 10;
    }


    private void LookFront()
    {
        isFront = true;
        transform.rotation = Quaternion.Euler(0, 90, 90);
        frontLight.SetActive(true);
        Vector3 v = new Vector3(transform.position.x, transform.position.y, 0);
        frontLightScript.setPosition(v);

        //playerScript.SeenByEnemy();
    }
}
