using UnityEngine;


public class SceanChanger : MonoBehaviour
{
    private string playerTag = "Player";
    private bool isRoaded;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag && !isRoaded)
        {
            StartCoroutine(GManager.Instance.SceanChange());
            isRoaded = true;
        }
    }

   
}
