using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerBase : MonoBehaviour
{
    [SerializeField] private GameObject imageOb;
    protected Image image;
    [SerializeField] private GameObject text;
    protected Text textBox;
    [SerializeField] private List<string> contents;
    protected GameObject player;
 
    // Start is called before the first frame update
    [System.Obsolete]
    private void Awake()
    {
        image = imageOb.GetComponent<Image>();
        textBox = text.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    internal IEnumerator Talk()
    {
        initiate();

        yield return null;
        for (int i =0; i<contents.Count; i++)
        {
            textBox.text = contents[i];
            yield return new WaitUntil(() => Input.anyKeyDown);
            yield return null;
        }
        image.enabled = false;
        textBox.enabled = false;

    }

    private void initiate()
    {
        image = imageOb.GetComponent<Image>();
        image.enabled = true;
        textBox = text.GetComponent<Text>();
        textBox.enabled = true;
    }
}