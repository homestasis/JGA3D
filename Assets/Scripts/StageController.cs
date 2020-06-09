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


    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        temp = TempController.Instance;
        gMane = GManager.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        /*if (stageClearTrigger != null && stageClearTrigger.IsPlayerOn() && !doClear)
        {
            StageClear();
            GManager.instance.stageNum = nextStageNum;
            SceneManager.LoadScene("Stage" + nextStageNum);
            doClear = true;
        }*/
        if (p.IsDieAnimEnd() && p.GameOver())
        {
            temp.IncreaseValue(continuePlus[gMane.continueNum]);
            playerObj.transform.position = continuePoint[0].transform.position;
            p.ContinuePlayer();
        }
        else if (p.IsDieAnimEnd())
        {
            PlayerSetContinuePoint();
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

    /*public void ChangeScene(int Num)
    {
        nextStageNum = Num;
    }*/

    /*public void StageClear()
    {
        GManager.instance.isStageClear = true;
        ChangeScene(GManager.instance.stageNum + 1);
    }*/
}
