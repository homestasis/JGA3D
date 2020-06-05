using UnityEngine;


public class WalkingFarmer : FarmerController
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

    private SpeechChange speech;
    private float moveDelta;


    protected override void Awake()
    {
        base.Awake();
        speech = transform.Find("SpeechBubble").gameObject.GetComponent<SpeechChange>();
    }

    private void Start()
    {
        LookLeft();
        speech.Turn(0);
        moveDelta = (float)(delta * Time.deltaTime);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isStop)
        {
            return;
        }

        if (isTurn)
        {
            float rot = (float)(90 * vec - vec * (sumTime / 0.3) * 180);
            transform.rotation = Quaternion.Euler(0, rot, 0);
            speech.Turn(0);

            sumTime += Time.deltaTime;
            if (sumTime > 0.3f)
            {
                startToWalk();
            }
        }

        if (isLeft)
        {
            float x = transform.position.x - moveDelta;
            if (x <= minX)
            {
                isLeft = false;
                LookBack();
            }
            else
            {
                transform.Translate(0, 0, moveDelta);

            }
        }
        if (isRight)
        {
            float x = transform.position.x + moveDelta;
            if (x >= maxX)
            {
                isRight = false;
                LookBack();
            }
            else
            {
                transform.Translate(0, 0, moveDelta);
            }
        }

    }

    internal override void LookToPlayer()
    {
        base.LookToPlayer();
        isStop = true;
    }

    internal override void ResetDirection()
    {
        base.ResetDirection();
        isStop = false;
    }

    private void LookBack()
    {
        isTurn = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        speech.Turn(0);
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
        vec = 1;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        speech.Turn(0);
    }
    private void LookLeft()
    {
        isLeft = true;
        vec = -1;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        speech.Turn(0);
    }

    internal void StopToMove()
    {
        isStop = true;
    }

    private void RestartToMove()
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
