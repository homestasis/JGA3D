using UnityEngine;

public class FireController : MonoBehaviour
{
    private Light fireLight;
    [SerializeField] private float recoveryTemp;
    private TempController tempSilder;

    private void Awake()
    {
        fireLight = GetComponent<Light>();
        tempSilder = TempController.Instance;
    }

   
    internal void PutOutFire()
    {
        fireLight.intensity = 0;
    }

    void OnTriggerStay(Collider other)
    {
        tempSilder.RecoveryTemp(recoveryTemp * Time.deltaTime);
    }
}
