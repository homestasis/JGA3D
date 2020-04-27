using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;

    private NewPlayerController p;


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

    }

    /// <summary>
    /// プレイヤーをコンティニューポイントへ移動する
    /// </summary>
    public void PlayerSetContinuePoint()
    {
        playerObj.transform.position = continuePoint[GManager.instance.continueNum].transform.position;
        p.ResetIsStop();
    }
}
