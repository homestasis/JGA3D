using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveFarmer : FarmerController
{
    private TempController tempC;
    [SerializeField]private GameObject black;
    private FadeImage fade;
      
	protected override void Awake()
	{
		base.Awake();
        tempC = TempController.Instance;
        fade = black.GetComponent<FadeImage>();
	}

	internal override void ResetDirection()
	{
		base.ResetDirection();

        StartCoroutine(Blacken());
	}

    private IEnumerator Blacken()
    {
        yield return StartCoroutine(fade.FadeOut());
        tempC.initiate();
        StartCoroutine(fade.FadeIn());
    }
}
