using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HinterTextController : MonoBehaviour
{
    [SerializeField] private List<string> hintScripts;
    private Text tex;


    private void Awake()
    {
        tex = GetComponent<Text>();
    }

    internal void UpdateText(int i)
    { 
        tex.text = hintScripts[i];
    }
}
