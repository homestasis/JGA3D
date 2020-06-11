using UnityEngine;

public class Enemy3 : EnemyBase
{
    private bool isStand;
    private bool isStop;
    private bool isFront;
    private bool isBack;
    private bool isFrontToBack;
    private bool isBackToFront;

    private float sumTime;

    private Collider colid;

    [SerializeField] private float pase;
    [SerializeField] private float turn;

    private void Start()
    {
        colid = colid = transform.Find("SeeSight").gameObject.GetComponent<Collider>();
        isFront = true;
    }

    private void Update()
    {
        if (isStop) { return; }


        sumTime += Time.deltaTime;
        if(isFront)
        {
            LookFront();
        }
        else if(isFrontToBack)
        {
            FrontToBack();
        }
        else if(isBack)
        {
            LookBack();
        }
        else if(isBackToFront)
        {
            BackToFront();
        }

    }

    private void LookBack()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (sumTime > pase)
        {
            isBack = false;
            isBackToFront = true;
            sumTime = 0;
            anim.SetBool("isTurn", true);
            colid.enabled = false;
        }
    }

    private void LookFront()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        if(sumTime > pase)
        {
            isFront = false;
            isFrontToBack = true;
            sumTime = 0;
            anim.SetBool("isTurn", true);
            colid.enabled = false;
        }
    }

    private void FrontToBack()
    {
        if (sumTime > turn)
         {
            isFrontToBack = false;
            isBack = true;
            sumTime = 0;
            anim.SetBool("isTurn", false);
            colid.enabled = true;
        }
        float rot = (float)(180 - (sumTime)/turn * 180);
        transform.rotation = Quaternion.Euler(0, rot, 0);
  
    }

    private void BackToFront()
    {
        if (sumTime > turn)
        {
            isBackToFront = false;
            isFront = true;
            sumTime = 0;
            anim.SetBool("isTurn", false);
            colid.enabled = true;
        }
        float rot = (float)((sumTime)/turn * 180);
        transform.rotation = Quaternion.Euler(0, rot, 0);
    }

    protected override void StopToMove()
    {
        isStop = true;
    }

    protected override void RestartToMove()
    {
        isStop = false;
    }
}
