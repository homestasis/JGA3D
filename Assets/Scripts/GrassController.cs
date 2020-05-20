using System.Collections;
using UnityEngine;


public class GrassController : MonoBehaviour
{

    [SerializeField] private float delta;
    private bool isGrow;


    internal IEnumerator GrowGrass()
    {
        if (transform.localScale.y > 10f){ isGrow = true; }
        float sum = 1f;
        while (!isGrow)
        {
            sum += delta;
            transform.localScale = new Vector3(1f, sum, 1f);
            if (sum >= 10f)
            {
                sum = 10f;
                transform.localScale = new Vector3(1f, sum, 1f);
                isGrow = true;
            }

            yield return new WaitForSeconds(0.05f);

        }
    }
}
