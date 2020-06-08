using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempController : SingletonMonoBehaviour<TempController>
{

    [SerializeField] private float nomalTempDecrease;
    [SerializeField] private float heavyTempDecrease;
    private float normalDecrease;
    private float heavyDecrease;

    private Slider tempSlider;

    protected override void Awake()
    {
        base.Awake();
        tempSlider = GetComponent<Slider>();
    }
    
    internal void initiate()
    {
        tempSlider.value = 1;
    }

    internal void HeavyDecrease()
    {
        tempSlider.value -= heavyDecrease*Time.deltaTime;
    }
    internal void NormalDecrease()
    {
        tempSlider.value -= normalDecrease*Time.deltaTime;
    }

    internal float GetValue()
    {
        return tempSlider.value;
    }

    internal void IncreaseValue(float plusValue)
    {
        tempSlider.value += plusValue;
        if(tempSlider.value >= 1) { tempSlider.value = 1; }
    }

    internal void RecoveryTemp(float plusTemp)
    {
        tempSlider.value += plusTemp;
    }

}
