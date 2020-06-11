using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UmbrellaController : MonoBehaviour
{
    [SerializeField] Sprite normalRainImage;
    [SerializeField] Sprite heavyRainImage;

    private Image umbImage;

    private void Awake()
    {
        umbImage = GetComponent<Image>();
    }

    internal void SetNormalRainImage()
    {
        umbImage.sprite = normalRainImage;
    }
    internal void SetHeavyRainImage()
    {
        umbImage.sprite = heavyRainImage;
    }
}
