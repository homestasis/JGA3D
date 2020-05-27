using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    private GameObject player;
    private Camera3DController cam;
  //  [SerializeField] private GameObject hinterSpeechBubble;
//    private HintMover hintScript;

    private bool[] isEnter;
    [SerializeField] private List<float> colliderPos;
    [SerializeField] private List<GameObject> zoomOb;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera3DController>();

        isEnter = new bool[2];
    }

    private void OnTriggerEnter(Collider other)
    {
        float posX = player.transform.position.x;

        for (int i = 0; i < 2; i++)
        {
            if (!isEnter[i] &&  posX - colliderPos[i] < 1)
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

    private IEnumerator Ob1()
    {
        cam.ZoomIn(zoomOb[0].transform.position, new Vector3(10, 0, 0));
        yield return new WaitForSeconds(1f);
        cam.ZoomAutoExtend(0.8f);
    }

    private IEnumerator Ob2()
    {
        cam.ZoomInExtend(zoomOb[1].transform.position, new Vector3(3, 0, 0),5, 0.8f);
        yield return new WaitForSeconds(1f);
        cam.ZoomAutoExtend(0.8f);
    }
}
