using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStage2Controller : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        if (posX > -32)
        {
            initPlace();
        }
        else
        {
            transform.position = new Vector3(posX, posY, -20);
        }
    }

    private void initPlace()
    {
        transform.position = new Vector3(-2, 6, -30);
        cam.fieldOfView = 60;
        transform.rotation = Quaternion.Euler(0, -10, 0);
    }
}
