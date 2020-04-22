using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    private GameObject cam;
    private GameObject player;
    private GameObject screen;

    private float worldScreenHeight;
    private float worldScreenWidth;

    private float screenLeftPos;
    private float screenBottomPos;
    private float screenRightPos;

    private float spriteLeftPos;
    private float spriteBottomPos;
    private float spriteRightPos;


    void Start()
    {
        cam = Camera.main.gameObject;
        player = GameObject.Find("Player");
        screen = GameObject.Find("screen1_back");
        SpriteRenderer sprite = screen.GetComponent<SpriteRenderer>();

        //中心からのHeight(全長の半分), widthも同様
        worldScreenHeight = Camera.main.orthographicSize;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        spriteLeftPos = sprite.transform.position.x - sprite.bounds.size.x / 2;
        spriteBottomPos = sprite.transform.position.y - sprite.bounds.size.y / 2;
        spriteRightPos = spriteLeftPos + sprite.bounds.size.x;

        updatePos();
    }

    void Update()
    {
        updatePos();  
    }


    public void updatePos()
    {
        adjustCameraToPlayer();

        updateScreenPointPos();

        if (screenLeftPos < spriteLeftPos)
        {
            adjustCameraToLeft();
        }
        if (screenBottomPos < spriteBottomPos)
        {
            adjustCameraToBottom();
        }
        if(screenRightPos > spriteRightPos)
        {
            adjustCameraToRight();
        }
    }


    private void adjustCameraToPlayer()
    {
        cam.transform.position = new Vector3(player.transform.position.x + offsetX,
            player.transform.position.y + offsetY, -1);
    }

    private void updateScreenPointPos()
    {
        screenLeftPos = cam.transform.position.x - worldScreenWidth;
        screenBottomPos = cam.transform.position.y - worldScreenHeight;
        screenRightPos = screenLeftPos + 2 * worldScreenWidth;
    }


    private void adjustCameraToLeft()
    {
        transform.position = new Vector3(spriteLeftPos + worldScreenWidth,
           cam.transform.position.y, -1);
    }

    private void adjustCameraToBottom()
    {
        transform.position = new Vector3(cam.transform.position.x,
            spriteBottomPos + worldScreenHeight, -1);
    }

    private void adjustCameraToRight()
    {
        transform.position = new Vector3(spriteRightPos - worldScreenWidth,
           cam.transform.position.y, -1);
    }
    
   
}
