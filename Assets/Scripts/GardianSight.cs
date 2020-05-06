using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardianSight : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject stageOb;
    private NewPlayerController pController;
    private EnemyGardian enemyGardian;
    private StageController stage;

    private void Awake()
    {
        pController = player.GetComponent<NewPlayerController>();
        GameObject gardian = transform.root.gameObject;
        enemyGardian = gardian.GetComponent<EnemyGardian>();
        stage = stageOb.GetComponent<StageController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FindingPlayer());
            /*if (pController.IntoSight())
            {
                StartCoroutine(FindingPlayer());
            }*/
        }
    }

    private IEnumerator FindingPlayer()
    {
        enemyGardian.FindPlayer();
        pController.StopPlayer();
        yield return new WaitForSeconds(1.4f);
        stage.PlayerSetContinuePoint();
        enemyGardian.FindAnimFalse();
    }
}
