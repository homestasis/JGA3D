using UnityEngine;

public class ClearController : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(GManager.Instance.SceanChange());
        }
    }
}
