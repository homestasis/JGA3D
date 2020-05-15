using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HinterTextController : MonoBehaviour
{
    [SerializeField] private List<string> hintScripts;
    private Text tex;
    private int num;

    private void Awake()
    {
        tex = GetComponent<Text>();
        num = 0;
    }

    internal void UpdateText()
    {
        tex.text = hintScripts[num];
        num++;
    }
}
