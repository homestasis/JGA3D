using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    [Header("ステージコントローラー")] public StageController ctrl;

    private string playerTag = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == playerTag)
        { 
            ctrl.PlayerSetContinuePoint();
        }
    }
}
