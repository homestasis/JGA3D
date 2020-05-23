using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    public PlayerTriggerOn stageClearTrigger;

    private NewPlayerController p;
    private int nextStageNum = 1;
    private bool doClear = false;


    // Start is called before the first frame update
    void Start()
    {
        if (playerObj != null && continuePoint != null && continuePoint.Length > 0)
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
        if (stageClearTrigger != null && stageClearTrigger.IsPlayerOn() && !doClear)
        {
            StageClear();
            GManager.instance.stageNum = nextStageNum;
            SceneManager.LoadScene("Stage" + nextStageNum);
            doClear = true;
        }
        if (p.IsDieAnimEnd())
        {
            playerObj.transform.position = continuePoint[GManager.instance.continueNum].transform.position;
            p.ContinuePlayer();
        }
    }

    /// <summary>
    /// プレイヤーをコンティニューポイントへ移動する
    /// </summary>
    public void PlayerSetContinuePoint()
    {
        playerObj.transform.position = continuePoint[GManager.instance.continueNum].transform.position;
        p.ResetIsStop();
    }

    public void ChangeScene(int Num)
    {
        nextStageNum = Num;
    }

    public void StageClear()
    {
        GManager.instance.isStageClear = true;
        ChangeScene(GManager.instance.stageNum + 1);
    }
}
