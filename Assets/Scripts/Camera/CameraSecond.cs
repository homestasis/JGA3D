using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CameraSecond : CameraBase
{

    private bool isSuperHigh;
    private bool inRoom;

    internal override void UpdatePos()
    {
        if(inRoom)
        {
            return;
        }

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;


        if (playerX <= 0)//開始地点
        {
            if (maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], -10);
            // Debug.Log("Low 1");
        }
        else if(!isHigh)
        {
            if(playerY >= 3f)
            {
                StartCoroutine(LowToHigh(playerX));
                // Debug.Log("Low To High");
            }
            else
            {
                maxX = -100;
                transform.position = new Vector3(playerX + offsetX, posY[0], -10);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                // Debug.Log("Low 2");
            }
        }
        else if (isHigh)
        {
            if(playerY < 3f)
            {
                StartCoroutine(HightToLow1(playerX));
                // Debug.Log("High to Low 1");
            }
            else if (!isSuperHigh && playerY > 9)
            {
                StartCoroutine(HighToSuperHigh(playerX));
            }
            else if(isSuperHigh && playerY < 9)
            {
                StartCoroutine(SuperHighToHigh(playerX));
            }
            else if(isSuperHigh)
            {
                transform.position = new Vector3(playerX + offsetX, 12, -10);
            }
            else
            {
                transform.position = new Vector3(playerX + offsetX, 8, -10);
            }
              
        }
        else {}
    }

    internal void RoomCamera()
    {
        inRoom = true;
        transform.position = new Vector3(204, 0, -4);
        cam.fieldOfView = 60;
    }

    internal void ResetInRoom()
    {
        inRoom = false;
        cam.fieldOfView = 50;
    }

    private IEnumerator LowToHigh(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 8, -10), 0.5f);
      //  transform.DORotate(new Vector3(5, 0, 0), 0.5f);
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
        transform.DOMove(new Vector3(playerX + offsetX, posY[0], -10), 0.5f);
      //  transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = false;
    }
    private IEnumerator HighToSuperHigh(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 12, -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isSuperHigh = true;
    }
    private IEnumerator SuperHighToHigh(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 8, -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isSuperHigh = false;
    }
}
