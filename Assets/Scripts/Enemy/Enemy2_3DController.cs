using UnityEngine;


public class Enemy2_3DController : EnemyBase
{
    protected override void RestartToMove()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
