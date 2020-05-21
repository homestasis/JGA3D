using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainSwitcher : SingletonMonoBehaviour<RainSwitcher>
{
    [SerializeField] private float maxCoolTime;
    private float coolTime;
    private Image umbrellaImage;
    private Image geugeImage;
    private bool active;
    private bool isHeavyRain;
    private UmbrellaController umbController;
    private RainSwitchBackGrounder backgrounder;

   
    protected override void Awake()
    {
        base.Awake();

        backgrounder = transform.Find("background").gameObject.GetComponent<RainSwitchBackGrounder>();
        umbController = transform.Find("umbrella").gameObject.GetComponent<UmbrellaController>();
        geugeImage = transform.Find("gauge").gameObject.GetComponent<Image>();

        SetActive();
    }

    // Update is called once per frame
    private void Update()
    {
        

        if(coolTime > 0)
        {
            coolTime -= Time.deltaTime;    
        }
        else if(coolTime <= 0 && !active)
        {
            SetActive();
        }
        UpdateGeuge();
    }

    internal void ChangeToHeavyRain()
    {
        umbController.SetNormalRainImage();
        UseSkill();
    }

    internal void ChangeToNormalRain()
    {
        umbController.SetHeavyRainImage();
        UseSkill();
    }

    internal bool GetIsActive()
    {
        return active;
    }

    private void UseSkill()
    {
        active = false;
        coolTime = maxCoolTime;
        backgrounder.SetCoolTimeImage();
    }

    private void SetActive()
    {
        active = true;
        coolTime = 0;
        backgrounder.SetActiveTimeImage();
    }

    private void UpdateGeuge()
    {
        geugeImage.fillAmount = (coolTime/maxCoolTime);
    }
}
