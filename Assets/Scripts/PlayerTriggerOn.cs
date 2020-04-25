using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerOn : MonoBehaviour
{
    private string playerTag = "Player";
    private BoxCollider col;
    private bool isOn = false;
    private bool isEnter, isStay, isExit;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        if (col == null)
        {
            Debug.Log("ボックスコライダーがついていません");
        }
    }

    public bool IsPlayerOn()
    {
        if (isEnter || isStay)
        {
            isOn = true;
        }
        else if (isExit)
        {
            isOn = false;
        }
        return isOn;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == playerTag)
        {
            isEnter = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == playerTag)
        {
            isStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == playerTag)
        {
            isExit = true;
        }
    }
}
