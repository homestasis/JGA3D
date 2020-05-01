using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    //住人の話す内容
    public string[] scenarios;
    //プレイヤーが範囲内にいるかどうかの判定
    bool Aflagflag = false;

    public IventScript iventScript;

    void OnTriggerEnter(Collider other)
    {
        //もしplayerタグをつけたゲームオブジェクトが住人のCollider判定範囲に入ったら
        if (other.gameObject.tag == "Player")
        {
            Aflagflag = true;
            //IventScriptのStartIventメソッドに会話が可能な状態であることを示すフラグと、住人の話す内容を送る。
            iventScript.StartIvent(Aflagflag, scenarios);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //プレイヤーが範囲外に出たら会話しない。 
            Aflagflag = false;
        }
    }
}
