using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTemp : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x +1, 9f, -10);
    }
}
