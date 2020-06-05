﻿using System.Collections;
using UnityEngine;


public class WaterController : MonoBehaviour
{ 
    [SerializeField] private float delta;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private NewPlayerController pController;

    private float initX;
    private float initZ;

    private bool isUp;

    private void Awake()
    {
        pController = NewPlayerController.Instance;
    }

    private void Start()
    {
        initX = transform.position.x;
        initZ = transform.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        pController.GetIntoWater(transform.position.y);
    }

    private void OnTriggerExit(Collider other)
    {
        pController.ResetWater();
    }




    internal virtual IEnumerator IncreaseWater()
    {
        isUp = true;
        if (transform.position.y >= maxY)
        {
           isUp = false;
        }
        float sum = minY;
        while (isUp)
        {
            sum += delta;
            transform.position = new Vector3(initX, sum, initZ);
            pController.setSurfaceP(sum);
            if (sum >= maxY)
            {
                sum = maxY;
                transform.position = new Vector3(initX, sum, initZ);
                isUp = false;
            }
            yield return null;
        }
       // Debug.Log("end increase Water");
    }

    internal virtual IEnumerator DecreaseWater()
    {
        isUp = false;
        if (transform.position.y <= minY) { isUp = true; }
        float sum = maxY;
        while (!isUp)
        {
            sum -= delta;
            transform.position = new Vector3(initX, sum, initZ);
            if (sum <= minY)
            {
                sum = minY;
                transform.position = new Vector3(initX, sum, initZ);
                isUp = true;
            }
            yield return null;
        }
    }
}
