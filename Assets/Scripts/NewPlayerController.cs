using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NewPlayerController : SingletonMonoBehaviour<NewPlayerController>
{
    public float speed;
    public float gravity;
    public float jumpSpeed;
    public float jumpHeight;
    public float jumpLimitTime;
    public float climbSpeed;
    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;
    // public LadderCheck ladderChecker;
    private LadderCheck ladderChecker;

    private Rigidbody rb = null;
    private CharacterController controller = null;
    private Animator anim = null;
    private bool isRun = false;
    private bool isJump = false;
    private bool isDead = false;
    private bool isLadder = false;
    private bool rainKey = false;
    private bool fall;
    private bool inWater;
    private bool isOnAir;
    private bool isLowTemp;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private float beforeKey;
    private float surfaceP;
    private float xSpeed;
    private float ySpeed;
    private float xSpeedBefore;
    private float jumpPoint;
    private bool isStop;
    private Vector3 StopPoint;
    private string deadTag = "DeadPoint";

    [SerializeField] private float speedPropInHeavyRain;
    private CameraBase cam;
    private AudioSource audio;
    private List<SpeechChange> speechScripts;
    private GameObject[] farmers;
    private List<FarmerController> farmerScripts;
    private RainSwitcher rainSwitcher;
    private LeverController lever;
    private TempController tempSlider;
    private PPController ppController;
    private WeatherController weather;
    private SpellSound spel;

    protected override void Awake()
    {
        base.Awake();

        weather = WeatherController.Instance;
        ladderChecker = GetComponent<LadderCheck>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        rainSwitcher = RainSwitcher.Instance;
        tempSlider = TempController.Instance;
        ppController = GameObject.Find("PostProcesser").GetComponent<PPController>();
        spel = transform.Find("Speller").gameObject.GetComponent<SpellSound>();

        initiateComponent();      

     
    }

    private void Start()
    {
        SetNormalRain();
        tempSlider.initiate();
    }


    private void initiateComponent()
    {
        //reload
        GameObject cameraOb = GameObject.FindWithTag("MainCamera");
        cam = cameraOb.GetComponent<CameraBase>();   
        speechScripts = new List<SpeechChange>();
        farmerScripts = new List<FarmerController>();
        farmers = GameObject.FindGameObjectsWithTag("Farmer");
        foreach (GameObject g in farmers)
        {
            GameObject speechBubble = g.transform.Find("SpeechBubble").gameObject;
            speechScripts.Add(speechBubble.GetComponent<SpeechChange>());
            farmerScripts.Add(g.GetComponent<FarmerController>());
        }        
    }

    internal void initiateStage2()
    {
        initiateComponent();
        lever = GameObject.FindWithTag("Lever").GetComponent<LeverController>();
    }



    private void Update()
    {
        Conversation();
        if (isStop)
        {
            Stay();
            return;
        }

        GetRain();
        DecreaseTempreture();
        float v = tempSlider.GetValue();
        if (v <= 0.3)
        {
            ppController.TurnOnMidEffect();
            isLowTemp = true;
        }
        else if(v <= 0.1)
        {
            ppController.TurnOnHeavyEffect();
        }
        else if (v <= 0)
        {
            //GameOver
        }
        else
        {
            if (isLowTemp)
            {
                isLowTemp = false;
                ppController.TurnOffEffect();
            }
        }

        isLadder = ladderChecker.IsLadder();
        IsOnAir();
        GetXSpeed();
        if (!inWater)
        {
            GetYSpeed();
        }
        else
        {
            audio.Stop();
            xSpeed = (float)(xSpeed * 0.8);
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

    public void Spell()
    {
        spel.Spell();
    }

    public void OnEventFx()
    {
        if(!rainKey)
        {
            rainSwitcher.ChangeToHeavyRain();
            StartCoroutine(weather.BeRainnyAsync());
            rainKey = true;
        }
        else if(rainKey)
        {
            rainSwitcher.ChangeToNormalRain();
            weather.BeSunny();
            rainKey = false;
        }
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

    internal bool IntoSight()
    {
        if (weather.GetIsHeavyRainy()) { return false; }

        return true;
    }

    internal void StopPlayer()
    {
        isStop = true;
        isRun = false;
        StopPoint = transform.position;
    }

    internal void ResetIsStop()
    {
        isStop = false;
    }

    private void Stay()
    {
        if(!controller.isGrounded)
        {
            Vector3 direction = new Vector3(0, -gravity, 0);
            controller.Move(direction * Time.deltaTime);
        }

        anim.SetBool("jump", false);
        anim.SetBool("run", false);
    }

    private void IsOnAir()
    {
        if (!controller.isGrounded && !inWater && !isLadder)
        {
            if (!isOnAir)
            {
                xSpeedBefore = xSpeed;
                isOnAir = true;
                jumpPoint = transform.position.y;
            }
        }
        else
        {
            if (isOnAir)
            {
                Fall();
            }
            isOnAir = false;
           
        }
    }

    private void Fall()
    {
        float dis = jumpPoint - transform.position.y;
        if (dis > 2.5 && !inWater)
        {
            StopPlayer();
            anim.Play("Die");
            isDead = true;
        }
    }

    private void DecreaseTempreture()
    {
        if (rainKey)
        {
            tempSlider.HeavyDecrease();
        }
        else
        {
            tempSlider.NormalDecrease();
        }
    }

   

    private void GetXSpeed()
    {
        float horizontalKey = Input.GetAxis("Horizontal");
        xSpeed = 0f;
        if (horizontalKey > 0)
        {

            xSpeed = speed;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            if (isLadder)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                dashTime += Time.deltaTime;
            }
            else
            {
                
                if(isOnAir)
                {
                    xSpeed = xSpeedBefore + (float)(speed * 0.3);
                    audio.Stop();
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                    dashTime += Time.deltaTime;
                }
            }
            
            isRun = true;
        }
        else if (horizontalKey < 0)
        {
            isRun = true;
            xSpeed = -speed;
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            if (isLadder)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                dashTime += Time.deltaTime;
            }
            else
            {
                if(isOnAir)
                {
                    xSpeed = xSpeedBefore - (float)(speed * 0.3);
                    audio.Stop();
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                    dashTime += Time.deltaTime;
                }
            }
           
        }
        else
        {
            audio.Stop();

            if(isOnAir)
            {
                xSpeed = xSpeedBefore;
            }
            else
            {
                isRun = false;
                xSpeed = 0.0f;
                dashTime = 0.0f;
            }
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
        bool jump = Input.GetKey(KeyCode.Space) || Input.GetButton("Jump");//aaaaa
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
        bool pushSpace = Input.GetKey(KeyCode.Space) || Input.GetButtonDown("Jump");//aaaaaa
        ySpeed = -gravity;
        if (!isJump)
        {
            if (pushSpace && jumpTime < jumpLimitTime && !fall)
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

            if(!fall)
            {
                float bouyanValue =(float)(gravity * (surfaceP - y) *6);
                ySpeed += bouyanValue;
            }
        }
    }
    private float CalculateBouyancy(float posY)
    {
        return (float)(gravity * (surfaceP - posY) / 0.10);
    }

    private void GetRain()
    {
        if((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Action1")) && rainSwitcher.GetIsActive())
        {
            anim.SetTrigger("BeRain");
        }

    }

    private void Conversation()
    {
        if(Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Action2"))
        {
            for(int i =0; i<speechScripts.Count; i++)
            {
                if (speechScripts[i].GetIsDisplay())
                {
                    StartCoroutine(StartToConversation(i));
                }
            }

            if (lever is null) { return; }
            else if (lever.GetIsDisplay())
            {
                anim.SetTrigger("lever");
                lever.Levering();
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
        cam.ZoomIn(pos, farmerScripts[i].GetEuler());
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == deadTag)
        {
            StopPlayer();
            anim.Play("Die");
            isDead = true;
        }
    }
    public bool IsDieAnimEnd()
    {
        if (isDead)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("Die"))
            {
                if (currentState.normalizedTime >= 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void ContinuePlayer()
    {
        ResetIsStop();
        isDead = false;
        anim.Play("Idol");
        isJump = false;
        isRun = false;
    }
}
