using UnityEngine;

public class KeyChecker : SingletonMonoBehaviour<KeyChecker>
{
    private bool isChanged;
    private GameObject canv;

    private bool isStart;


    protected override void Awake()
    {
        base.Awake();
        canv = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    private void Update()
    {
        if(isStart && (Input.anyKeyDown || Input.GetButtonDown("Action2")))
        {
            if (!isChanged)
            {
                isChanged = true;
                StartCoroutine(GManager.Instance.Reset());
                Destroy(canv);
            }
        }
    }

    internal void SetIsStart()
    {
        isStart = true;
    }
}
