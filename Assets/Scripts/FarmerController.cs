using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerController : FarmerBase
{
   
    internal void LookToPlayer()
    {
        transform.LookAt(player.transform);
    }

    internal void ResetDirection()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
