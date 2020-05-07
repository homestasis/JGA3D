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

    private void Start()
    {
        recoveryTemp = recoveryTemp * Time.deltaTime;
    }
    internal void PutOutFire()
    {
        fireLight.intensity = 0;
    }

    void OnTriggerStay(Collider other)
    {
        pController.RecoveryTemp(recoveryTemp);
    }
}
