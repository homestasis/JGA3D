using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        GameObject root = transform.root.gameObject;
        anim = root.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("isFind", true);
        //GameOver();
    }
}
