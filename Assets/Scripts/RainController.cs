using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainController : MonoBehaviour
{
    AudioSource audioSource;
    GameObject player;
    
    [Header("初期音量")]
    public float INIT_VOLUME;
    [Header("音を弱める(強める)場所のX座標")]
    public List<float> weekPointX;
    public List<float> strongPointX;
    [Header("最低(最高)音量")]
    public float MIN_VOLUME;
    public float MAX_VOLUME;
    [Header("音が弱(強)まり始める点との距離")]
    public float NEIGHBORHOOD;
    [Header("動くもの")]
    public List<GameObject> movingOb;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        audioSource.volume = INIT_VOLUME;
    }

    // Update is called once per frame
    void Update() 
    {
        AtFixedPoint();
        MovingSounds();
    }

    private void AtFixedPoint()
    {
        foreach (float loc in weekPointX)
        {
            float pPos = player.transform.position.x;
            float gap = loc - pPos;
            DecreaseVolume(gap);

            gap = player.transform.position.x - loc;
            DecreaseVolume(gap);
        }

        foreach(float loc in strongPointX)
        {
            float pPos = player.transform.position.x;
            float gap = loc - pPos;
            IncreaseVolume(gap);

            gap = pPos - loc;
            IncreaseVolume(gap);
        }
    }

    private void MovingSounds()
    {
        foreach (GameObject ob in movingOb)
        {
            float obPos = ob.transform.position.x;
            float pPos = player.transform.position.x;
            float gap = obPos - pPos;
            IncreaseVolume(gap);
            gap = pPos - obPos;
            IncreaseVolume(gap);
        }
    }

    private void DecreaseVolume(float gap)
    {
        if (gap >= 0 && gap <= NEIGHBORHOOD)
        {
            audioSource.volume = (float)(INIT_VOLUME - (INIT_VOLUME - MIN_VOLUME) * System.Math.Sin(1 - gap / NEIGHBORHOOD));
        }
    }

    private void IncreaseVolume(float gap)
    {
        if (gap >= 0 && gap <= NEIGHBORHOOD)
        {
            audioSource.volume = (float)(INIT_VOLUME + (MAX_VOLUME - INIT_VOLUME) * System.Math.Sin(1 - gap / NEIGHBORHOOD)); 
        }
    }
  
}
