using System.Collections;


public class WaterGimmickController : WaterController
{
    private bool isUnder;

    internal override IEnumerator DecreaseWater()
    {
        if (isUnder) { StartCoroutine(base.DecreaseWater()); }
        yield break;
    }
 
    internal void DecreaseByLever()
    {
        StartCoroutine(base.DecreaseWater());
        isUnder = true;
    }

    internal void SetIsUnder(bool b)
    {
        isUnder = b;
    }
    internal bool GetIsUnder(bool b)
    {
        return isUnder; 
    }
}
