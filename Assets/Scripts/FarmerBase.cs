using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerBase : MonoBehaviour
{
    [SerializeField] private GameObject imageOb;
    private Image image;
    private Text textBox;
    private List<string> contents;

    // Start is called before the first frame update
    [System.Obsolete]
    private void Awake()
    {
        image = imageOb.GetComponent<Image>();
        GameObject text = gameObject.transform.FindChild("Text").gameObject;
        textBox = text.GetComponent<Text>();

    }

    public  IEnumerator Talk()
    {
        initiate();
       
        foreach (string cont in contents)
        {
            textBox.text = cont;
            yield return new WaitUntil(() => Input.GetKeyDown(0));
            yield return null;
        }

        image.enabled = false;
        textBox.enabled = false;
        
    }

    private void initiate()
    {
        image.enabled = true;
        textBox.enabled = true;
    }
}