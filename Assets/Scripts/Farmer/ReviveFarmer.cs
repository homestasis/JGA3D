using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveFarmer : FarmerController
{
    private TempController tempC;
      
	protected override void Awake()
	{
		base.Awake();
        tempC = TempController.Instance;
	}

	internal override void ResetDirection()
	{
		base.ResetDirection();
        tempC.initiate();
	}
}
