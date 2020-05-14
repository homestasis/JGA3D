using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GrassController : MonoBehaviour
{

    [SerializeField] private float delta;


    internal async void GrowGrass()
    {
        if (transform.localScale.y > 10f){ return;}
        float sum = 1f;
        while (true)
        {
            sum += delta;
            transform.localScale = new Vector3(1f, sum, 1f);
            if (sum >= 10f)
            {
                sum = 10f;
                transform.localScale = new Vector3(1f, sum, 1f);
                return;
            }
            
            await Task.Delay(50);
           
        }
    }
}
