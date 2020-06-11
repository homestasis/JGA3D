﻿using System.Collections;
using UnityEngine;

public class EnemySeeSight : MonoBehaviour
{
    [SerializeField] private GameObject stageOb;
    private NewPlayerController pController;
    private EnemyBase enemyBase;
    private StageController stage;

    private bool isFind;


    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pController = player.GetComponent<NewPlayerController>();
        GameObject enemy = transform.parent.gameObject;
        enemyBase = enemy.GetComponent<EnemyBase>();
        stage = stageOb.GetComponent<StageController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isFind)
        {
            if (pController.IntoSight())
            {
                isFind = true;
                StartCoroutine(FindingPlayer());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isFind)
        {
            if (pController.IntoSight())
            {
                isFind = true;
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
