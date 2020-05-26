using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLever : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    internal void LeverUp()
    {
        anim.Play("LeverUp");
    }
    internal void LeverDown()
    {
        anim.Play("LeverDown");
    }
}
