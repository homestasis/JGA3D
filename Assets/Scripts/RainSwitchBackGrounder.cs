using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainSwitchBackGrounder : MonoBehaviour
{
    [SerializeField] Sprite coolTimeImage;
    [SerializeField] Sprite activeTImeImage;

    private Image backImage;


    private void Awake()
    {
        backImage = GetComponent<Image>();
    }

    internal void SetCoolTimeImage()
    {
        backImage.sprite = coolTimeImage;
    }

    internal void SetActiveTimeImage()
    {
        backImage.sprite = activeTImeImage;
    }
}
