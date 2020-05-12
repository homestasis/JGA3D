using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightController : MonoBehaviour
{
    private Light pLight;
    private bool isOn;
    private AudioSource spellaudio;

    private void Awake()
    {
        pLight = GetComponent<Light>();
        spellaudio = GetComponent<AudioSource>();
    }

    internal void TurnOff()
    {
        pLight.intensity = 0f;
        isOn = false;
    }

    internal void TurnOn()
    {
        pLight.intensity = 1f;
        isOn = true;
    }

    internal bool GEtisOn()
    {
        return isOn;
    }

    internal void Spell()
    {
        spellaudio.Play();
    }
}
