﻿using System.Collections;
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

    protected virtual void Awake()
    {
        image = imageOb.GetComponent<Image>();
        textBox = text.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        initEular = transform.rotation;
        talkMob = GetComponent<TalkMobController>();
    }

    internal IEnumerator Talk()
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

    private void initiate()
    {
        image = imageOb.GetComponent<Image>();
        image.enabled = true;
        textBox = text.GetComponent<Text>();
        textBox.enabled = true;
    }

    
}
