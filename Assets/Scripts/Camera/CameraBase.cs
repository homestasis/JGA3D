using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class CameraBase : MonoBehaviour
{
    [SerializeField] private protected float offsetX;
    [SerializeField] private protected List<float> posY;
    [SerializeField] private protected GameObject player;
    [SerializeField] private protected LadderCheck ladder;

    private protected float maxX;
    private protected float posZ;

    private protected Camera cam;
    private protected float defaultFov;
    private protected float zoom;
    private protected float waitTime;

    private protected bool isZoom;
    private protected bool isHigh;
    private protected bool isNotTitle;

    internal void ZoomIn(Vector3 pos, Vector3 euler)
    {
        isZoom = true;
        transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov / zoom, waitTime);
    }

    internal void ZoomAuto()
    {
        isZoom = false;
        DOTween.To(() => Camera.main.fieldOfView, fov => Camera.main.fieldOfView = fov, defaultFov, waitTime);
        UpdatePos();

    }


    // Start is called before the first frame update
    private void Start()
    {
        maxX = -100;
        cam = GetComponent<Camera>();

        defaultFov = cam.fieldOfView;
        zoom = 6;
        waitTime = 0.4f;
        transform.position = new Vector3(-1, 5.2f, -2);
        transform.rotation = Quaternion.Euler(-20.6f, -17.2f, 2.2f);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isZoom) { return; }
        UpdatePos();

    }

    internal abstract void UpdatePos();
}
