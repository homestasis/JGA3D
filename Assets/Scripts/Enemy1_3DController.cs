using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;


public class Enemy1_3DController : MonoBehaviour
{

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float delta;

    private bool isLeft;
    private bool isRight;
    private bool isTurn;
    private bool isLook;

    private Animator anim;

    private float sumTime;


    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        isLeft = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isTurn)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            sumTime += Time.deltaTime;
            if(sumTime>0.3f)
            {
                startToWalk();
            }
        }

        if (isLeft)
        {
            float x = transform.position.x - delta;
            if(x <= minX)
            {
                isLeft = false;
                LookBack();
            }
            else
            {
                transform.Translate(0, 0, delta);

            }
        }
        if(isRight)
        {
            float x = transform.position.x + delta;
            if(x >= maxX)
            {
                isRight = false;
                LookBack();        
            }
            else
            {
                transform.Translate(0, 0, delta);
            }
        }

        SetAnimation();

    }

    private void　LookBack()
    {
        isTurn = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        sumTime = 0;
    }

    private void startToWalk()
    {
        sumTime = 0;
        isTurn = false;
        if (transform.position.x > (minX + maxX) / 2)
        {
            LookLeft();
        }
        else
        {
            LookRight();
        }
    }

    private void LookRight()
    {
        isRight = true;
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    private void LookLeft()
    {
        isLeft = true;
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    private void SetAnimation()
    {
        anim.SetBool("isLook", isLook);
    }
}
