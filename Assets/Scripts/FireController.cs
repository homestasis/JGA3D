using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private Light fireLight;

    private void Awake()
    {
        fireLight = GetComponent<Light>();
    }

    internal void PutOutFire()
    {
        fireLight.enabled = false;
    }
}
