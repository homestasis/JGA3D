using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3DController : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private List<float> posY;
    [SerializeField] private GameObject player;

    private float maxX;
    private float posZ;

    // Start is called before the first frame update
    void Start()
    {
        maxX = -100;
        posZ = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
       
        if (playerX >= 0)
        {
            if(maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], posZ);
            print("pos0");
            
        }
        else if(playerX < -19.0 && -20.8 <= playerX && playerY > 5.5)
        {
            transform.position = new Vector3(playerX + offsetX, playerY, posZ);
            print("pos2");
        }
        else if(playerX <= -20.8 && playerX >= -40)
        {
            transform.position = new Vector3(playerX + offsetX, 10, -4);
            transform.rotation = Quaternion.Euler(30, 0, 0);
            print("pos3");
        }
        else
        {
            maxX = -100;
            transform.position = new Vector3(playerX + offsetX, posY[0], posZ);
            print("pos1");
        }
    }


   

}
