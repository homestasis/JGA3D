using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GrassController : MonoBehaviour
{

    [SerializeField] private float delta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    public async void GrowGrass()
    {
        if (transform.localScale.y > 10f){ return;}
        float sum = 2f;
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
