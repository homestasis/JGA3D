using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenCheck : MonoBehaviour
{
    private string seenTag = "EnemyView";
    private bool isSeen = false;
    private bool isSeenEnter, isSeenStay, isSeenExit;

    public bool IsSeen()
    {
        if (isSeenStay)
        {
            isSeen = true;
        }
        else if(isSeenEnter || isSeenExit)
        {
            isSeen = false;
        }
        isSeenEnter = false;
        isSeenStay = false;
        isSeenExit = false;
        return isSeen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == seenTag)
        {
            isSeenEnter = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == seenTag)
        {
            isSeenStay = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == seenTag)
        {
            isSeenExit = true;
        }
    }
}
