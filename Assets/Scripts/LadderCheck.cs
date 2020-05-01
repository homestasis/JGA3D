using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCheck : MonoBehaviour
{
    private string ladderTag = "Ladder";
    private bool isLadder = false;
    private bool isLadderEnter, isLadderStay, isLadderExit;

    public bool IsLadder()
    {
        if (isLadderStay)
        {
            isLadder = true;
        }
        else if (isLadderEnter || isLadderExit)
        {
            isLadder = false;
        }

        isLadderEnter = false;
        isLadderStay = false;
        isLadderExit = false;
        return isLadder;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == ladderTag)
        {
            isLadderEnter = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == ladderTag)
        {
            isLadderStay = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == ladderTag)
        {
            isLadderExit = true;
        }
    }
}
