using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuePoint : MonoBehaviour
{
    [Header("コンティニュー番号")] public int continueNum;
    [Header("プレイヤー判定")] public PlayerTriggerOn trigger;

    private bool on = false;

    void Start()
    {
        //初期化
        if (trigger == null)
        {
            Debug.Log("インスペクターの設定が足りません");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーが範囲内に入った
        if (trigger.IsPlayerOn() && !on)
        {
            GManager.instance.continueNum = continueNum;
            on = true;
        }
    }
}
