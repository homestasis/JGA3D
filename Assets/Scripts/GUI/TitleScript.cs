using System.Collections;
using UnityEngine;


public class TitleScript : MonoBehaviour
{
    //public FadeImage fade;
    private Camera3DController cam;
    private Canvas rainCanvas;
    private Canvas tempCanvas;
    private NewPlayerController p;
    private FadeImage title;
    private TitleWord word;

    private bool firstPush;
    private float playerX;

    private void Awake()
    {
        GameObject cameraOb = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject titleLogo = transform.Find("TitleLogo").gameObject;
        GameObject titleWord = transform.Find("Word").gameObject;
        GameObject tempUI = GameObject.Find("TempUI");
        GameObject rainSwitch = GameObject.Find("RainSwitch");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        title = titleLogo.GetComponent<FadeImage>();
        word = titleWord.GetComponent<TitleWord>();
        tempCanvas = tempUI.GetComponent<Canvas>();
        rainCanvas = rainSwitch.GetComponent<Canvas>();
        p = player.GetComponent<NewPlayerController>();
        cam = cameraOb.GetComponent<Camera3DController>();

        playerX = player.transform.position.x;
    }

    private void Start()
    {
        rainCanvas.enabled = false;
        tempCanvas.enabled = false;

        StartCoroutine(StartToFade());
    }

    // Update is called once per frame
    void Update()
    {

        if(firstPush)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Action2"))
        {
            StartCoroutine(TitleDestroy());
            firstPush = true;
        }
       
    }

    private IEnumerator StartToFade()
    {
        yield return StartCoroutine(title.FadeOut());
        word.StartToFlash();
    }

    private IEnumerator TitleDestroy()
    {
        word.Stop();
        StartCoroutine(BGMSounder.Instance.FadeIn());
        StartCoroutine(title.FadeIn());
        yield return StartCoroutine(cam.PlayStart(playerX));

        rainCanvas.enabled = true;
        tempCanvas.enabled = true;

     
        
        p.enabled = true;  
        Destroy(gameObject);
    }

}