using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private List<float> posY;
    [SerializeField] private GameObject player;

    private float posZ;
    private float maxX;
    private float currentY;
    // Start is called before the first frame update
    void Start()
    {
        maxX = -100;
        posZ = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        

        if (playerX >= 0)
        {
            if (maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], posZ);
            //print("pos0");
        }
        {
            transform.position = new Vector3(playerX + offsetX, playerY + 1, posZ);
        }
    }
}
