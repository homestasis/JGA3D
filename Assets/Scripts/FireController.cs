using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private Light fireLight;
    private NewPlayerController pController;
    [SerializeField] private float recoveryTemp;

    private void Awake()
    {
        fireLight = GetComponent<Light>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<NewPlayerController>();
    }
    internal void PutOutFire()
    {
        fireLight.intensity = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        pController.RecoveryTemp(recoveryTemp);
    }
}
