using Coffee.UIExtensions;
using UnityEngine;
using UnityEngine.UI;


public class RainSwitchBackGrounder : MonoBehaviour
{ 
    private  Sprite coolTimeImage;
    private  Sprite activeTImeImage;

    private bool isActivate;
    private bool increase;

    private Image backImage;
    private UIHsvModifier uIHsvModifier;


    private void Awake()
    {
        installImage();
        backImage = GetComponent<Image>();
        uIHsvModifier = GetComponent<UIHsvModifier>();
    }

    private void installImage()
    {
        coolTimeImage =  Resources.Load<Sprite>("gray4");
        activeTImeImage = Resources.Load<Sprite>("blue4");
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
        if(GetComponent<Image>() == null) { Debug.Log("image is null"); }
        if(backImage == null) { Debug.Log("backImge is null");}
        if(backImage.sprite == null) { Debug.Log("sprite is null"); }
        if(activeTImeImage == null) { Debug.Log("active time image is null"); }
        backImage.sprite = activeTImeImage;
        isActivate = true;
    }
}
