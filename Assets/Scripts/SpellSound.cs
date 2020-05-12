using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSound : MonoBehaviour
{
    private AudioSource spellaudio;

    // Start is called before the first frame update
    void Start()
    {
        spellaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Spell()
    {
        spellaudio.Play();
    }
}
