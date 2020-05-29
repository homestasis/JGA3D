using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
    private string deadTag = "DeadPoint";

    [SerializeField] private float speedPropInHeavyRain;
    private CameraBase cam;
    private AudioSource audio;
    private List<SpeechChange> speechScripts;
    private GameObject[] farmers;
    private List<FarmerController> farmerScripts;
    [SerializeField] private GameObject handLight;
    private PointLightController handLightController;
    private RainSwitcher rainSwitcher;
    private LeverController lever;
    private TempController tempSlider;
    private PPController ppController;
 
    private void Awake()
    {
        initiateComponent();      

        SetNormalRain();
    }

    private void Start()
    {
        tempSlider.initiate();
        //stage2だったら的な
        if (GManager.instance.stageNum == 2)
        {
            lever = GameObject.FindWithTag("Lever").GetComponent<LeverController>();
        }
    }


    private void initiateComponent()
    {
        GameObject cameraOb = GameObject.FindWithTag("MainCamera");
        cam = cameraOb.GetComponent<CameraBase>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        speechScripts = new List<SpeechChange>();
        farmerScripts = new List<FarmerController>();
        farmers = GameObject.FindGameObjectsWithTag("Farmer");
        foreach (GameObject g in farmers)
        {
            GameObject speechBubble = g.transform.Find("SpeechBubble").gameObject;
            speechScripts.Add(speechBubble.GetComponent<SpeechChange>());
            farmerScripts.Add(g.GetComponent<FarmerController>());
        }
        weather = weatherOb.GetComponent<WeatherController>();
        rainSwitcher = RainSwitcher.Instance;
        tempSlider = TempController.Instance;

        handLightController = handLight.GetComponent<PointLightController>();

        ppController = GameObject.Find("PostProcesser").GetComponent<PPController>();
    }

    private void Update()
    {
        Conversation();
        if (isStop)
        {
            transform.position = StopPoint;
            return;
        }

        GetLightKey();

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

    public void Spell()
    {
        handLightController.Spell();
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


    private void IsOnAir()
    {
        if (!controller.isGrounded && !inWater && !isLadder)
        {
            if (!isOnAir)
            {
                xSpeedBefore = xSpeed;
                isOnAir = true;
                //Debug.Log("ON AIR");
            }
        }
        else
        {
            isOnAir = false;
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
        bool jump = Input.GetKey(KeyCode.Space);
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
        bool pushSpace = Input.GetKey(KeyCode.Space);
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

    private void GetLightKey()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if(handLightController.GEtisOn())
            {
                handLightController.TurnOff();
            }
            else
            {
                handLightController.TurnOn();
            }
        }
    }

    private void GetRain()
    {
        if(Input.GetKeyDown(KeyCode.Return) && rainSwitcher.GetIsActive())
        {
            anim.SetTrigger("BeRain");
        }

    }

    private void Conversation()
    {
        if(Input.GetKeyDown(KeyCode.A))
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
        ResetIsStop();
        farmerScripts[i].ResetDirection();
        
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
