using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmerController : MonoBehaviour
{
    [SerializeField] protected GameObject imageOb;
    protected Image image;
    [SerializeField] protected GameObject text;
    protected Text textBox;
    [SerializeField] protected List<string> contents;
    protected GameObject player;
    [SerializeField] protected Vector3 zoomEular;
    protected Quaternion initEular;

    protected TalkMobController talkMob;

    private static AudioClip[] farmAudio;
    private AudioSource audioComp;

    protected virtual void Awake()
    {
        image = imageOb.GetComponent<Image>();
        textBox = text.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        initEular = transform.rotation;
        talkMob = GetComponent<TalkMobController>();
        audioComp = GetComponent<AudioSource>();
        installAudio();
    }

    private static void installAudio()
    {
        if(farmAudio != null)
        {
            return;
        }

        farmAudio = new AudioClip[3];
        for(int i = 0; i<farmAudio.Length; i++)
        {
            farmAudio[i] = Resources.Load<AudioClip>("Audio/Farmer_sample"+(i+1));
        }
    }

    internal virtual IEnumerator Talk()
    {
        initiate();

        yield return null;
        for (int i = 0; i < contents.Count; i++)
        {
            textBox.text = contents[i];
            PlayVoice();
            yield return new WaitUntil(() => Input.anyKeyDown);
            yield return null;
        }
        image.enabled = false;
        textBox.enabled = false;
    }

    internal virtual void LookToPlayer()
    {
        transform.LookAt(player.transform);
    }

    internal virtual void ResetDirection()
    {
        transform.rotation = initEular;
        if (talkMob != null)
        {
            talkMob.MobGo();
        }
    }

    internal Vector3 GetEuler()
    {
        return zoomEular;
    }

    protected void initiate()
    {
        image.enabled = true;
        textBox.enabled = true;
    }

    private void PlayVoice()
    {
        int value = Random.Range(0, farmAudio.Length);
        audioComp.clip = farmAudio[value];
        audioComp.Play();
    }

    
}
