using UnityEngine;
using UnityEngine.UI;

public class TempController : SingletonMonoBehaviour<TempController>
{

    [SerializeField] private float nomalTempDecrease;
    [SerializeField] private float heavyTempDecrease;
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
        tempSlider.value -= heavyTempDecrease*Time.deltaTime;
    }
    internal void NormalDecrease()
    {
        tempSlider.value -= nomalTempDecrease*Time.deltaTime;
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
