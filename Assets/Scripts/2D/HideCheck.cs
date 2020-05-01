using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCheck : MonoBehaviour
{
    private string hideTag = "Hide";
    private bool isOnWall = false;
    private bool isWallEnter, isWallStay, isWallExit;

    public bool IsOnWall()
    {
        if (isWallStay)
        {
            isOnWall = true;
        }
        else if (isWallEnter || isWallExit)
        {
            isOnWall = false;
        }

        isWallEnter = false;
        isWallStay = false;
        isWallExit = false;
        return isOnWall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == hideTag)
        {
            isWallEnter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == hideTag)
        {
            isWallStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == hideTag)
        {
            isWallExit = true;
        }
    }
}
