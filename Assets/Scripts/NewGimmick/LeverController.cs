using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject puddle;
    private WaterController wController;
    private LeverLever lev;
    private ParticleSystem waterParticle;
    private SpeechChange speechBubble;

    private bool isUp;

    private void Awake()
    {
        wController = puddle.GetComponent<WaterController>();
        lev = transform.Find("Lever").gameObject.GetComponent<LeverLever>();
        waterParticle = transform.Find("Water_Drop").gameObject.GetComponent<ParticleSystem>();
        speechBubble = transform.Find("SpeechBubble").gameObject.GetComponent<SpeechChange>();
    }

    internal void Levering()
    {
        if(isUp)
        {
            LeverDown();
        }
        else
        {
            LeverUp();
        }
    }

    private void LeverUp()
    {
        isUp = true;
        lev.LeverUp();

        waterParticle.Stop();

        StartCoroutine(wController.DecreaseWater());
    }
    private void LeverDown()
    {
        isUp = false;
        lev.LeverDown();

        waterParticle.Play();

        StartCoroutine(wController.IncreaseWater());
    }

    internal bool GetIsDisplay()
    {
        return speechBubble.GetIsDisplay();
    }



}
