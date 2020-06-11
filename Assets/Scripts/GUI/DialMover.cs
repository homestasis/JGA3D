using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialMover : MonoBehaviour
{
    private float initY;
    private float initX;
    private Slider slide;
    private TempValueChanger textChanger;

    private void Awake()
    {
        GameObject TempBar = GameObject.Find("TempBar");
        slide = TempBar.GetComponent<Slider>();

        initX = transform.position.x;
        initY = transform.position.y;

        GameObject tempFill = transform.Find("TempFill").gameObject;
        textChanger = tempFill.GetComponent<TempValueChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = slide.value;
        transform.position = new Vector3(initX,CaluculatePos(value),0);
        textChanger.updateValue(value);

    }

    private float CaluculatePos(float value)
    {
        return (float)(initY - 0.133 * (1 - value)*1000);
    }
}
