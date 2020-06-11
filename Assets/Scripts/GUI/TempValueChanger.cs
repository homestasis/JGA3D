using UnityEngine;
using UnityEngine.UI;
using System;


public class TempValueChanger : MonoBehaviour
{
    private Text tex;


    private void Awake()
    {
        tex = GetComponent<Text>();
    }

    internal void updateValue(float value)
    {
        int t = (int)(value * 60);
        tex.text = t.ToString();
    }

}
