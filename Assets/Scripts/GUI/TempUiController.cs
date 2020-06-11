using UnityEngine;


public class TempUiController : SingletonMonoBehaviour<TempUiController>
{

    private Canvas canv;

    protected override void Awake()
    {
        base.Awake();
        canv = GetComponent<Canvas>();
    }

    internal void Off()
    {
        canv.enabled = false;
    }

    internal void On()
    {
        canv.enabled = true;
    }
}
