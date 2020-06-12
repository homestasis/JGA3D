using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class GManager : SingletonMonoBehaviour<GManager>
{
    public int continueNum;
    public int stageNum = 1;


    private string playerTag = "Player";
    private GameObject black;
    private FadeImage fade;
    private RainSwitcher rain;
    private TempUiController temp;
    private GManager gameManager;
    private NewPlayerController pController;
    private WeatherController weather;
    private BGMSounder bgm;

    protected override void Awake()
    {
        base.Awake();
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
        rain = RainSwitcher.Instance;
        temp = TempUiController.Instance;
        pController = NewPlayerController.Instance;
        weather = WeatherController.Instance;
        bgm = BGMSounder.Instance;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        stageNum = 1;
    }


    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
       
        if (string.Equals(nextScene.name, "Stage2"))
        {
            bgm.SetVolume(0.03f);
            StartStage2();
        }
        else if(string.Equals(nextScene.name, "Stage3"))
        {
            bgm.SetVolume(0.03f);
            StartCoroutine(Clear());
        }
        else if(string.Equals(nextScene.name, "Stage1"))
        {
            StartStage1();
        }
    }

    internal IEnumerator SceanChange()
    {

        rain.Off();
        temp.Off();
        StartCoroutine(bgm.FadeInComp());
        yield return StartCoroutine(fade.FadeOut());
        stageNum += 1;
        continueNum = 0;
        SceneManager.LoadScene("Stage" + stageNum);

    }

    internal IEnumerator Reset()
    {
        yield return StartCoroutine(fade.FadeOut());
        stageNum = 1;
        continueNum = 0;
        SceneManager.LoadScene("Stage" + stageNum);
    }

    private void StartStage2()
    {
        black = GameObject.Find("Black");
        StartCoroutine(fade.FadeIn());
        pController.enabled = true;
        pController.initiateStage2();
        SunLightController.Instance.initiate();
        WeatherController.Instance.initiate();

        rain.On();
        temp.On();

    }

    private IEnumerator Clear()
    {
        black = GameObject.Find("Black");
        yield return StartCoroutine(fade.FadeIn());

        DestroyThem();

        KeyChecker.Instance.SetIsStart();

    }

    private void StartStage1()
    {
        initiate();
        StartCoroutine(fade.FadeIn());
    }

    private void DestroyThem()
    {
        rain.DestroyThis();
        temp.DestroyThis();
        pController.DestroyThis();
        SunLightController.Instance.DestroyThis();
        WeatherController.Instance.DestroyThis();
        PPController.Instance.DestroyThis();
    }

    private void initiate()
    {
        black = GameObject.Find("Black");
        rain = RainSwitcher.Instance;
        temp = TempUiController.Instance;
        pController = NewPlayerController.Instance;
        bgm.initiate();
    }

}
