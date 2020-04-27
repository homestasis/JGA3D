﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    //シナリオを格納
    private string[] scenarios2;
    //uiTextへの参照を保つ
    private Text uiText;

    //現在の行番号
    int currentLine;

    //セリフを表示するテクスト(文字の大きさや色はここにアタッチしたTextをいじって決定する)
    public Text message;
    int flag = 0;
    //テキストウィンドウ
    public GameObject panel;

    // Use this for initialization
    public void StartText(string[] scenarios)
    {
        flag = 1;
        scenarios2 = scenarios;
        currentLine = 0;
        uiText = message;
        //パネルを表示
        panel.SetActive(true);
        //テキストを表示
        uiText.gameObject.SetActive(true);
        TextUpdate();

    }

    public void Click()
    {
        if (flag == 1)
        {
            //現在の行番号がラストまで行ってない状態でボタンを押すとテキストを更新する
            if (currentLine < scenarios2.Length)
            {
                TextUpdate();
            }
            else
            {
                //最後まで行ったら、テキストとテキストウィンドウを消す
                uiText.gameObject.SetActive(false);
                panel.SetActive(false);
                flag = 0;
            }
        }

    }


    void TextUpdate()
    {
        //現在の行番号をuiTextに流し込み、現在の行番号を一つ追加する
        uiText.text = scenarios2[currentLine];
        currentLine++;
    }

}