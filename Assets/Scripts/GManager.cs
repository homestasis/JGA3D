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
    }

    internal IEnumerator SceanChangeToStage2()
    {
        StartCoroutine(fade.FadeOut());
        rain.Off();
        temp.Off();
        pController.enabled = false;
        yield return new WaitForSeconds(3f);
        stageNum++;
        continueNum = 0;
        SceneManager.LoadScene("Stage" + stageNum);
    }

    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (string.Equals(nextScene.name, "Stage2"))
        {
            StartStage2();
        }
    }

    private void StartStage2()
    {
        black = GameObject.Find("Black");
        StartCoroutine(fade.FadeIn());
        pController.enabled = true;
        pController.initiateStage2();
        rain.enabled = true;
        temp.enabled = true;

    }

}
