using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HintMover : MonoBehaviour
{
    private RectTransform hintPos;
    private HinterTextController hinterText;

    private void Awake()
    {
        hintPos = GetComponent<RectTransform>();
        GameObject hintTextOb = transform.Find("HinterText").gameObject;
        hinterText = hintTextOb.GetComponent<HinterTextController>();
    }

    internal IEnumerator TurnOn(int n)
    {
        hinterText.UpdateText(n);
        yield return StartCoroutine(MoveIn());

        yield return new WaitForSeconds(6f);

        StartCoroutine(MoveOut());
    }

    private IEnumerator MoveIn()
    {
        int pos = 1240;
        while(pos != 680)
        {
            pos -= 20;
            hintPos.localPosition = new Vector3(pos, 350, 0);
            yield return new WaitForSeconds(0.005f);
        }
    }

    private IEnumerator MoveOut()
    {
        int pos = 680;
        while (pos != 1240)
        {
            pos += 20;
            hintPos.localPosition = new Vector3(pos, 350, 0);
            yield return new WaitForSeconds(0.005f);
        }
    }


}
