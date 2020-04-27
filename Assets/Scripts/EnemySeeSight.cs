﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeeSight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stageOb;
    private NewPlayerController pController;
    private EnemyBase enemyBase;
    private StageController stage;


    private void Awake()
    {
        pController = player.GetComponent<NewPlayerController>();
        GameObject enemy = transform.root.gameObject;
        enemyBase = enemy.GetComponent<EnemyBase>();
        stage = stageOb.GetComponent<StageController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pController.IntoSight())
            {
                StartCoroutine(FindingPlayer());
            }
        }
    }

    private IEnumerator FindingPlayer()
    {
        enemyBase.FindPlayer();
        pController.StopPlayer();
        yield return new WaitForSeconds(1.4f);
        stage.PlayerSetContinuePoint();
        enemyBase.GetAnimationFalse();
    }
}
