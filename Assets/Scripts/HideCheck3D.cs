using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCheck3D : MonoBehaviour
{
    [SerializeField] private GameObject playerOb;

    //PlayerのTagを変更
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOb.tag = "Player";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOb.tag = "Hide";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hide"))
        {
            playerOb.tag = "Player";
        }
    }
}
