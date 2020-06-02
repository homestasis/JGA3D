using UnityEngine;


public class CaveChecker : MonoBehaviour
{ 
    private SunLightController sun;

    private void Awake()
    {
        sun = SunLightController.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sun.EnterDarkPlace();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sun.ExitDarkPlace();
        }
    }
}
