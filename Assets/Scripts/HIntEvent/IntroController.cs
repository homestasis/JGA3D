using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    private GameObject player;
    private NewPlayerController pController;
    private Camera3DController cam;
    //  [SerializeField] private GameObject hinterSpeechBubble;
    //    private HintMover hintScript;

    [SerializeField] protected GameObject imageOb;
    protected Image image;
    [SerializeField] protected GameObject text;
    protected Text textBox;

    private bool[] isEnter;
    [SerializeField] private List<float> colliderPos;
    [SerializeField] private List<GameObject> zoomOb;
    [SerializeField] protected List<string> contents;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        pController = player.GetComponent<NewPlayerController>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera3DController>();
        image = imageOb.GetComponent<Image>();
        textBox = text.GetComponent<Text>();

        isEnter = new bool[2];
    }

    private void OnTriggerEnter(Collider other)
    {
        float posX = player.transform.position.x;

        for (int i = 0; i < 2; i++)
        {
            if (!isEnter[i] && posX - colliderPos[i] > -1 && posX - colliderPos[i] < 1)
            {
                isEnter[i] = true;
                switch (i)
                {
                    case 0: StartCoroutine(Ob1());break;
                    case 1: StartCoroutine(Ob2());break;
                    default:break;
                }
                
                break;

            }
        }

    }


    private IEnumerator Talk()
    {
        initiate();

        yield return null;
        for (int i = 0; i < contents.Count; i++)
        {
            textBox.text = contents[i];
            yield return new WaitUntil(() => Input.anyKeyDown);
            yield return null;
        }
        image.enabled = false;
        textBox.enabled = false;
    }

    private IEnumerator Ob1()
    {
        pController.Stop();
        cam.ZoomIn(zoomOb[0].transform.position, new Vector3(10, 0, 0));
        yield return null;
        yield return StartCoroutine(Talk());
        cam.ZoomAutoExtend(0.8f);
        pController.UnStop();
    }

    private IEnumerator Ob2()
    {
        pController.Stop();
        cam.ZoomInExtend(zoomOb[1].transform.position, new Vector3(3, 0, 0),5, 0.8f);
        yield return new WaitForSeconds(1f);
        cam.ZoomAutoExtend(0.8f);
        pController.UnStop();
    }



    private void initiate()
    {
        image = imageOb.GetComponent<Image>();
        image.enabled = true;
        textBox = text.GetComponent<Text>();
        textBox.enabled = true;
    }
}
