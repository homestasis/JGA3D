using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HintController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject hinterSpeechBubble;
    private HintMover hintScript;

    private bool[] isEnter;
    [SerializeField] private List<float> colliderPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        hintScript = hinterSpeechBubble.GetComponent<HintMover>();
        isEnter = new bool[colliderPos.Count];
    }

    private void OnTriggerEnter(Collider other)
    {
        float posX = player.transform.position.x;

        for (int i = 0; i < isEnter.Length; i++)
        {
            if (!isEnter[i] && posX - colliderPos[i] > 0 && posX - colliderPos[i] < 3)
            {
                isEnter[i] = true;
                StartCoroutine(hintScript.TurnOn(i));
                break;

            }
        }

    }

}
