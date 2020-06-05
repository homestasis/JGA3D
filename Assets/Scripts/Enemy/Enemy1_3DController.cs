using UnityEngine;


public class Enemy1_3DController : EnemyBase
{

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float delta;

    private bool isLeft;
    private bool isRight;
    private bool isTurn;
    private bool isStop;

    private float sumTime;
    private int vec;//Left = -1, Right = 1;

    private Collider colid;

    protected override void Awake()
    {
        base.Awake();
        colid = transform.Find("SeeSight").gameObject.GetComponent<Collider>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        LookLeft();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isStop)
        {
            return;
        }

        float moveVal = delta * Time.deltaTime;

        if (isTurn)
        {
            float rot = (float)(90 * vec - vec*(sumTime / 0.3) * 180);
            transform.rotation = Quaternion.Euler(0, rot, 0);

            sumTime += Time.deltaTime;
            if(sumTime>0.3f)
            {
                startToWalk();
            }
        }

        if (isLeft)
        {
            float x = transform.position.x - moveVal;
            if(x <= minX)
            {
                isLeft = false;
                LookBack();
            }
            else
            {
                transform.Translate(0, 0, moveVal);

            }
        }
        if (isRight)
        {
            float x = transform.position.x + moveVal;
            if (x >= maxX)
            {
                isRight = false;
                LookBack();
            }
            else
            {
                transform.Translate(0, 0, moveVal);
            }
        }

    }

    private void　LookBack()
    {
        isTurn = true;
        colid.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        sumTime = 0;
    }

    private void startToWalk()
    {
        sumTime = 0;
        isTurn = false;
        colid.enabled = true;
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
        vec = 1;
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    private void LookLeft()
    {
        isLeft = true;
        vec = -1;
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    protected override void  StopToMove()
    {
        isStop = true;
    }

    protected override void RestartToMove()
    {
        isStop = false;
        if (isLeft)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (isRight)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
}
