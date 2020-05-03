using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WaterController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float delta;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private NewPlayerController pController;

    private float initX;
    private float initZ;

    private void Awake()
    {
        pController = player.GetComponent<NewPlayerController>();
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
        pController.ResetIsStop();
    }



    internal async void IncreaseWater()
    {
        if (transform.position.y >= maxY) { return; }
        float sum = minY;
        while (true)
        {
            sum += delta;
            transform.position = new Vector3(initX, sum, initZ);
            pController.setSurfaceP(sum);
            if (sum >= maxY)
            {
                sum = maxY;
                transform.position = new Vector3(initX, sum, initZ);
                return;
            }
            await Task.Delay(50);
        }
    }

    internal async void DecreaseWater()
    {
        if (transform.position.y <= minY) { return; }
        float sum = maxY;
        while (true)
        {
            sum -= delta;
            transform.position = new Vector3(initX, sum, initZ);
            if (sum <= minY)
            {
                sum = minY;
                transform.position = new Vector3(initX, sum, initZ);
                return;
            }
            await Task.Delay(50);
        }
    }
}
