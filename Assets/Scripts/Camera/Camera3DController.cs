using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera3DController : CameraBase
{
   
    internal override void UpdatePos()
    {

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;

        if (!isNotTitle)
        {
            //transform.position = new Vector3(-1,5,-2);
            //transform.rotation = Quaternion.Euler(-20,-14,2);
        }
        else if (playerX <= 0)//開始地点
        {
            if (maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], -10);
            // Debug.Log("Low 1");
        }
        else if (!isHigh && playerY >= 7f)
        {
            StartCoroutine(LowToHigh(playerX));
            // Debug.Log("Low To High");
        }
        else if (!isHigh && playerX < 40)
        {
            maxX = -100;
            transform.position = new Vector3(playerX + offsetX, posY[0], -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // Debug.Log("Low 2");
        }
        else if (isHigh && playerY < 7f)
        {
            if (playerX < 40)
            {
                StartCoroutine(HightToLow1(playerX));
                // Debug.Log("High to Low 1");
            }
            else
            {
                StartCoroutine(HightToLow2(playerX));
                // Debug.Log("High to Low 2");
            }
        }
        else if (isHigh)
        {
            transform.position = new Vector3(playerX + offsetX, 10, -10);
            //Debug.Log("High");
        }
        else if (playerX > 40 && !isHigh)
        {
            transform.position = new Vector3(playerX + offsetX, 8, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // Debug.Log("Low 3");
        }
        else
        {

        }
    }

    private IEnumerator LowToHigh(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 10, -10), 0.5f);
        transform.DORotate(new Vector3(5, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = true;
    }

    private IEnumerator HightToLow1(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, posY[0], -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = false;
    }


    private IEnumerator HightToLow2(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 8, -10), 0.5f);
        transform.DORotate(new Vector3(0,0,0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = false;
    }

    public IEnumerator PlayStart(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, posY[0], -10), 3f);
        transform.DORotate(new Vector3(0, 0, 0), 3f);
        yield return new WaitForSeconds(3f);
        isNotTitle = true;
    }
}
