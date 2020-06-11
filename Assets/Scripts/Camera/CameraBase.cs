﻿using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CameraBase : MonoBehaviour
{
    [SerializeField] private protected float offsetX;
    [SerializeField] private protected List<float> posY;
    protected GameObject player;

    private protected float maxX;
    private protected float posZ;

    private protected Camera cam;
    private protected float defaultFov;
    private protected float zoom;
    private protected float waitTime;

    private protected bool isZoom;
    private protected bool isHigh;


    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        maxX = -100;
        cam = GetComponent<Camera>();

        defaultFov = cam.fieldOfView;
        zoom = 6;
        waitTime = 0.4f;
    }

    internal void ZoomIn(Vector3 pos, Vector3 euler)
    {
        isZoom = true;
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov / zoom, waitTime);
    }

    internal void ZoomInExtend(Vector3 pos, Vector3 euler, float locZoom, float locTime)
    {
        isZoom = true;
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov / locZoom, locTime);
    }

    internal void ZoomAuto()
    {
        isZoom = false;
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov, waitTime);
        UpdatePos();

    }

    internal void ZoomAutoExtend(float locTime)
    {
        isZoom = false;
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov, locTime);
        UpdatePos();
    }


 

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isZoom) { return; }
        UpdatePos();

    }

    internal abstract void UpdatePos();
}
