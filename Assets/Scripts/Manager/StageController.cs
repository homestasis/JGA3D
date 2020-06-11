using UnityEngine;


public class StageController : MonoBehaviour
{
    private GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("コンテニュー体温プラス量")]
    [SerializeField] private int[] continuePlus; 
  //  public PlayerTriggerOn stageClearTrigger;

    private NewPlayerController p;
    private int nextStageNum = 1;
    private bool doClear = false;
    private TempController temp;
    private GManager gMane;
    private GameObject black;
    private FadeImage fade;

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        temp = TempController.Instance;
        gMane = GManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        black = GameObject.Find("Black");
        fade = black.GetComponent<FadeImage>();
        if (continuePoint != null && continuePoint.Length > 0)
        {
            playerObj.transform.position = continuePoint[0].transform.position;

            p = playerObj.GetComponent<NewPlayerController>();
            if (p == null)
            {
                Debug.Log("プレイヤーが設定されていません");
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("ステージコントローラーの設定が足りていません");
            Destroy(this);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (p.IsDieAnimEnd() && p.GameOver())
        {
            StartCoroutine(fade.FadeOut());
            if (fade.CompFadeOut())
            {             
                temp.IncreaseValue(continuePlus[0]);
                playerObj.transform.position = continuePoint[0].transform.position;
                p.ContinuePlayer();
                StartCoroutine(fade.FadeIn());
            }
        }
        else if (p.IsDieAnimEnd())
        {
            PlayerSetContinuePoint();
            BGMSounder.Instance.SetVolume(0.03f);
        }
    }

    /// <summary>
    /// プレイヤーをコンティニューポイントへ移動する
    /// </summary>
    public void PlayerSetContinuePoint()
    {
        temp.IncreaseValue(continuePlus[gMane.continueNum]);
        playerObj.transform.position = continuePoint[gMane.continueNum].transform.position;
        p.ContinuePlayer();
    }
}
