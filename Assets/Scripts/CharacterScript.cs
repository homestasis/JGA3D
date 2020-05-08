using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class CharacterScript : MonoBehaviour
{

    [SerializeField] private GameObject speechBubble;
    private SpeechChange speech;

    private void Awake()
    {
        speech = speechBubble.GetComponent<SpeechChange>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            speech.SetIsDisplay();
            StartCoroutine(speech.SetImage());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            speech.ResetIsDisplay();
        }
    }
}
