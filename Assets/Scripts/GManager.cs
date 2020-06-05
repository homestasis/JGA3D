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

    protected override void Awake()
    {
        base.Awake();
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
        rain = RainSwitcher.Instance;
        temp = TempUiController.Instance;
        pController = NewPlayerController.Instance;
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
            StartStage2();
        }
        else if(string.Equals(nextScene.name, "Stage3"))
        {
            Clear();
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
        yield return StartCoroutine(fade.FadeOut());
        stageNum += 1;
        if (stageNum == 4)
        {
            stageNum = 1;
        }
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

    private void Clear()
    {
        black = GameObject.Find("Black");
        StartCoroutine(fade.FadeIn());
    }

    private void StartStage1()
    {
        black = GameObject.Find("Black");
        StartCoroutine(fade.FadeIn());
    }

}
