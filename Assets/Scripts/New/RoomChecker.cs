using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    private CameraSecond cam;
    private SunLightController sun;

    private void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraSecond>();
        sun = SunLightController.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cam.RoomCamera();
            sun.EnterDarkPlace();
        }
     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cam.ResetInRoom();
            sun.ExitDarkPlace();
        }
    }
}
