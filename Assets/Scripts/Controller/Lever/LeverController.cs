using UnityEngine;


public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject puddle;
    private WaterGimmickController wController;
    private LeverLever lev;
    private ParticleSystem waterParticle;
    private SpeechChange speechBubble;

    private bool isUp;

    private void Awake()
    {
        wController = puddle.GetComponent<WaterGimmickController>();
        lev = transform.Find("Lever").gameObject.GetComponent<LeverLever>();
        waterParticle = transform.Find("Water_Drop").gameObject.GetComponent<ParticleSystem>();
        speechBubble = transform.Find("SpeechBubble").gameObject.GetComponent<SpeechChange>();
    }

    internal void Levering()
    {
        if(isUp)
        {
            LeverDown();
        }
        else
        {
            LeverUp();
        }
    }

    private void LeverUp()
    {
        isUp = true;
        lev.LeverUp();

        waterParticle.Stop();

        wController.DecreaseByLever();
    }
    private void LeverDown()
    {
        isUp = false;
        lev.LeverDown();

        waterParticle.Play();

        StartCoroutine(wController.IncreaseWater());
        wController.SetIsUnder(true);
    }

    internal bool GetIsDisplay()
    {
        return speechBubble.GetIsDisplay();
    }



}
