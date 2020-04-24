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


    // Start is called before the first frame update
    private void Start()
    {
        isLeft = true;

    }

    // Update is called once per frame
    private void Update()
    {
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

    }

    private void　LookBack()
    {
        isTurn = true;
        anim.SetBool("isTurn", isTurn);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void startToWalk()
    {
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
        anim.SetBool("isTurn", isTurn);
    }
}
