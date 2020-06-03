using UnityEngine;


public class SceanChanger : MonoBehaviour
{
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag)
        {
            StartCoroutine(GManager.Instance.SceanChangeToStage2());
        }
    }

   
}
