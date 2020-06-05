using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    private bool isChanged;
    private GameObject canv;


    private void Awake()
    {
        canv = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            if (!isChanged)
            {
                isChanged = true;
                StartCoroutine(GManager.Instance.SceanChange());
                Destroy(canv);
            }
        }
    }
}
