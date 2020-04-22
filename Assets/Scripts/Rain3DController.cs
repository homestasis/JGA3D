using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Rain3DController : MonoBehaviour
{

    [SerializeField] private float delta;
    private AudioSource audioSource;

    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    internal async void StartToSoundRain()
    {
        if (audioSource.volume >= 0.5f) { return; }
        while (true)
        {
            audioSource.volume += delta;
            if (audioSource.volume >= 0.5f)
            {
                audioSource.volume = 0.5f;
                return;
            }

            await Task.Delay(50);
        }
    }

    internal async void StopToSoundRain()
    {
        if(audioSource.volume <= 0) { return; }
        while (true)
        {
            audioSource.volume -= delta;
            if (audioSource.volume <= 0f)
            {
                audioSource.volume = 0f;
                return;
            }

            await Task.Delay(50);
        }
    }
}
