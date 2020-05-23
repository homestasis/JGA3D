﻿using UnityEngine;


public class EnemyBase : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private Enemy1_3DController eController;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        eController = GetComponent<Enemy1_3DController>();
    }

    internal void FindPlayer()
    {
        transform.LookAt(player.transform);
       // transform.localRotation = Quaternion.Euler(0, 180, 0);
        eController.StopToMove();
        anim.SetBool("isLook", true);
    }

    internal void GetAnimationFalse()
    {
        anim.SetBool("isLook", false);
        RestartToMove();
    }

    protected virtual void RestartToMove()
    {

    }



}
