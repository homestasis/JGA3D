﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    //住人の話す内容
    public string[] scenarios;
    //プレイヤーが一定範囲内に入ったら会話できるサインを表すオブジェクト
    public GameObject sign;
    //プレイヤーが範囲内にいるかどうかの判定
    bool Aflagflag = false;

    public IventScript iventScript;

    void Start()
    {
        sign.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //もしplayerタグをつけたゲームオブジェクトが住人のCollider判定範囲に入ったら
        if (other.gameObject.tag == "player")
        {
            Aflagflag = true;
            //IventScriptのStartIventメソッドに会話が可能な状態であることを示すフラグと、住人の話す内容を送る。
            iventScript.StartIvent(Aflagflag, scenarios);

            //住人の頭上にオブジェクトを表示(座標は自由に変えてください)
            sign.transform.position = gameObject.transform.position + new Vector3(-0.5f, 1f, 0);
            sign.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            //プレイヤーが範囲外に出たら会話しない。 
            Aflagflag = false;
            sign.SetActive(false);
        }
    }
}
