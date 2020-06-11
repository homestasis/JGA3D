using UnityEngine;

public class HideCheck3D : MonoBehaviour
{
    private GameObject playerOb;

    private void Awake()
    {
        playerOb = GameObject.FindWithTag("Player");
    }

    //PlayerのTagを変更
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOb.tag = "Player";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOb.tag = "Hide";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hide"))
        {
            playerOb.tag = "Player";
        }
    }
}
