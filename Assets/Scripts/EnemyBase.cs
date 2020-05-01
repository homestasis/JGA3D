using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerController pController;
    private Animator anim;
    private Enemy1_3DController eController;

    private void Awake()
    {
        pController = player.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        eController = GetComponent<Enemy1_3DController>();
    }

    internal void FindPlayer()
    {
        transform.LookAt(player.transform);
        eController.StopToMove();
        anim.SetBool("isLook", true);
    }

    internal void GetAnimationFalse()
    {
        anim.SetBool("isLook", false);
    }



}
