using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class WaterController : MonoBehaviour
{
    [SerializeField] private float delta;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private float initX;
    private float initZ;
     
    private void Start()
    {
        initX = transform.position.x;
        initZ = transform.position.z;
    }

    internal async void IncreaseWater()
    {
        if (transform.position.y >= maxY) { return; }
        float sum = minY;
        while (true)
        {
            sum += delta;
            transform.position = new Vector3(initX, sum, initZ);
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
