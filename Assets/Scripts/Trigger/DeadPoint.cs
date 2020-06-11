using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    [Header("ステージコントローラー")] public StageController ctrl;

    private string playerTag = "Player";
    private GameObject player;
    private NewPlayerController p;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        p = player.GetComponent<NewPlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == playerTag)
        {
            //ctrl.PlayerSetContinuePoint();
        }
    }
}
