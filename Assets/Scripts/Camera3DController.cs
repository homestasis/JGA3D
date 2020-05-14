using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera3DController : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private List<float> posY;
    [SerializeField] private GameObject player;
    [SerializeField] private LadderCheck ladder;

    private float maxX;
    private float posZ;

    private Camera cam;
    private float defaultFov;
    private float zoom;
    private float waitTime;

    private bool isZoom;
    private bool isHigh;


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
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (isZoom) { return; }
        UpdatePos();

    }

    private void UpdatePos()
    {

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;


        if (playerX <= 0)//開始地点
        {
            if (maxX < 0) { maxX = playerX; }
            transform.position = new Vector3(maxX + offsetX, posY[0], -10);
            // Debug.Log("Low 1");
        }
        else if (!isHigh && playerY >= 7f)
        {
            StartCoroutine(LowToHigh(playerX));
            // Debug.Log("Low To High");
        }
        else if (!isHigh && playerX < 40)
        {
            maxX = -100;
            transform.position = new Vector3(playerX + offsetX, posY[0], -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // Debug.Log("Low 2");
        }
        else if (isHigh && playerY < 7f)
        {
            if (playerX < 40)
            {
                StartCoroutine(HightToLow1(playerX));
                // Debug.Log("High to Low 1");
            }
            else
            {
                StartCoroutine(HightToLow2(playerX));
                // Debug.Log("High to Low 2");
            }
        }
        else if (isHigh)
        {
            transform.position = new Vector3(playerX + offsetX, 10, -10);
            //Debug.Log("High");
        }
        else if (playerX > 40 && !isHigh)
        {
            transform.position = new Vector3(playerX + offsetX, 8, -10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            // Debug.Log("Low 3");
        }
        else
        {

        }
    }

    private IEnumerator LowToHigh(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 10, -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = true;
    }

    private IEnumerator HightToLow1(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, posY[0], -10), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = false;
    }


    private IEnumerator HightToLow2(float playerX)
    {
        transform.DOMove(new Vector3(playerX + offsetX, 10, -10), 0.5f);
        transform.DORotate(new Vector3(10,0,0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isHigh = false;
    }

}
