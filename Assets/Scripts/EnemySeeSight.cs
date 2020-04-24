using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeeSight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private NewPlayerController pController;

    private void Awake()
    {
        pController = player.GetComponent<NewPlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           /*Playerの関数よんで
            *Playerのほうで隠れてるか判定する
            *
            *
            */
        }
    }
}
