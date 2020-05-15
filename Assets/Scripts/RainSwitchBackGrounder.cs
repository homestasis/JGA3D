using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;


public class RainSwitchBackGrounder : MonoBehaviour
{
    [SerializeField] Sprite coolTimeImage;
    [SerializeField] Sprite activeTImeImage;

    private bool isActivate;
    private bool increase;

    private Image backImage;
    private UIHsvModifier uIHsvModifier;


    private void Awake()
    {
        backImage = GetComponent<Image>();
        uIHsvModifier = GetComponent<UIHsvModifier>();
    }

    private void Update()
    {
        if (isActivate)
        {

            float delta = (float)(Time.deltaTime * 0.6);
            if (increase)
            {
                uIHsvModifier.value += delta;
            }
            else
            {
                uIHsvModifier.value -= delta;
            }

            if(uIHsvModifier.value >= 0)
            {
                increase = false;
            }
            else if(uIHsvModifier.value <= -0.35)
            {
                increase = true;
            }
        }
    }

    internal void SetCoolTimeImage()
    {
        backImage.sprite = coolTimeImage;
        isActivate = false;
    }

    internal void SetActiveTimeImage()
    {
        backImage.sprite = activeTImeImage;
        isActivate = true;
    }
}
