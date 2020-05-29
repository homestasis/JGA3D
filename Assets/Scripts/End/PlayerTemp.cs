using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour
{
    public float delta; 

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
    }
}
