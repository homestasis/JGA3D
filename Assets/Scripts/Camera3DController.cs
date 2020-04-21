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
       
        if (playerX >= 0)
        {
            if(maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], posZ);
            
        }
        else
        {
            maxX = -100;
            transform.position = new Vector3(playerX + offsetX, posY[0], posZ);
        }
    }


   

}
