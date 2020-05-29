using System.Collections;
using UnityEngine;

public class ShoppingFarmer : FarmerController
{
    private static Vector3[] place;
    private static bool[] isVacant;
    private static Vector3[] standingDir;
    [SerializeField] private int myPlaceNum;
    private Animator anim;
    private static string[] trueContents;
    private static string[] falseContents;
    private NewPlayerController pController;
    private Collider colid;
    private SpeechChange speech;

    protected override void Awake()
    {
        base.Awake();
        initiateStatic();
        anim = GetComponent<Animator>();
        pController = player.GetComponent<NewPlayerController>();
        colid = GetComponent<Collider>();
        speech = transform.Find("SpeechBubble").gameObject.GetComponent<SpeechChange>();
    }

    private void Start()
    {
        transform.position = place[myPlaceNum];
        Turn();
    }

    internal override IEnumerator Talk()
    {
        initiate();

        string playingString = isVacant[myPlaceNum + 1] ? trueContents[myPlaceNum] : falseContents[myPlaceNum];

        yield return null;
        
        textBox.text = playingString;
        yield return new WaitUntil(() => Input.anyKeyDown);
        yield return null;
        
        image.enabled = false;
        textBox.enabled = false;
    }

    internal override void ResetDirection()
    {
        if (myPlaceNum != 7 && isVacant[myPlaceNum + 1])
        {
            StartCoroutine(Move());
            updateVacancy();
        }
        else
        {
            transform.rotation = initEular;
        }
    }

    private static void initiateStatic()
    {
        if(place != null) { return; }
        place = new Vector3[8]{
          new Vector3(75, -1.2f,0),
          new Vector3(73, -1.2f, 0.7f),
          new Vector3(72.5f,-1.2f, -0.7f),
          new Vector3(71f,-1.2f,0),
          new Vector3(66.3f, -1.2f, 0.7f),
          new Vector3(68.7f,-1.2f, 0.7f),
          new Vector3(67.8f,-1.2f,-0.7f),
          new Vector3(59, -1.2f, -0.7f)
        };

        isVacant = new bool[8]
        {
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true
        };

        standingDir = new Vector3[8]
        {
            new Vector3(0,-40,0),
            new Vector3(0,0,0),
            new Vector3(0,180,0),
            new Vector3(0,-40,0),
            new Vector3(0,0,0),
            new Vector3(0,0,0),
            new Vector3(0,0,0),
            new Vector3(0,0,0)
        };

        trueContents = new string[8]
        {
            "オ アイタ アイタ",
            "コレ ミルカイ",
            "カサ ハ コンド デ イイヤ",
            "アイタゾー",
            "ツギハ トナリ ミタイ",
            "ドイテ ホシイカイ",
            "アッチ イコ",
            ""
        };

        falseContents = new string[8]
        {
            "ジャマダナー",
            "ムムムムム",
            "ナニイロ ガ イイカナ",
            "チカクデ ミタインダヨネ",
            "コレ カッタホウガ イイカナ",
            "イイツボダネ",
            "チョット キュウケイ",
            "ヤスイ モノ オオイヨ"
        };

        
    }

    private IEnumerator Move()
    {
        Vector3 nextPlace =place[myPlaceNum + 1];
        Vector3 dir = nextPlace - place[myPlaceNum];
        float dis = Vector3.Distance(nextPlace, place[myPlaceNum]);
        myPlaceNum++;

        colid.enabled = false;

        transform.LookAt(nextPlace);
        anim.SetBool("run", true);
        pController.StopPlayer();
        while (true)
        {
            transform.position += dir * Time.deltaTime/dis;
            if (Vector3.Distance(nextPlace, transform.position)<0.1)
            {
                transform.position = nextPlace;
                break;
            }
            yield return null;
        }
        anim.SetBool("run", false);
        Turn();
        pController.ResetIsStop();
        colid.enabled = true;
    }

    private void updateVacancy()
    {
        isVacant[myPlaceNum] = false;
        isVacant[myPlaceNum - 1] = true;
        
    }

    private void Turn()
    {
        transform.rotation = Quaternion.Euler(standingDir[myPlaceNum]);
        speech.Turn(0);
    }

}
