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
    private void Start()
    {
        normalDecrease = (float)(nomalTempDecrease * Time.deltaTime);
        heavyDecrease = (float)(heavyTempDecrease * Time.deltaTime);
    }

    internal void initiate()
    {
        tempSlider.value = 1;
    }

    internal void HeavyDecrease()
    {
        tempSlider.value -= heavyDecrease;
    }
    internal void NormalDecrease()
    {
        tempSlider.value -= normalDecrease;
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
