using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyGardian : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float xMoov = 0;
    [SerializeField] private float speed = 0;

    private Rigidbody rb = null;
    private Animator anim = null;
    private bool isRun = false;
    private bool isLook = false;
    private float startPosX;
    private float nextPosX;
    private float pos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        startPosX = transform.position.x;
        nextPosX = transform.position.x + xMoov;
    }

    private void Update()
    {
        SetAnimation();
        pos = transform.position.x;
        if (xMoov < 0 && pos < nextPosX)
        {
            transform.position = new Vector3(nextPosX, transform.position.y, transform.position.z);
            isRun = false;
        }
        else if (xMoov > 0 && pos > nextPosX)
        {
            transform.position = new Vector3(nextPosX, transform.position.y, transform.position.z);
            isRun = false;
        }
        if (xMoov < 0 && pos > startPosX)
        {
            transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            isRun = false;
        }
        else if(xMoov > 0 && pos < startPosX)
        {
            transform.position = new Vector3(startPosX, transform.position.y, transform.position.z);
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            isRun = false;
        }
    }

    internal async void RunAway()
    {
        isRun = true;
        if (xMoov < 0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            rb.velocity = new Vector3(-speed, 0, 0);
        }
        else if (xMoov > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            rb.velocity = new Vector3(speed, 0, 0);
        }
    }

    internal async void ComeBack()
    {
        isRun = true;
        if (xMoov < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (xMoov > 0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            rb.velocity = new Vector3(-speed, 0, 0);
        }
    }

    internal void FindPlayer()
    {
        isLook = true;
    }

    internal void FindAnimFalse()
    {
        isLook = false;
    }

    private void SetAnimation()
    {
        anim.SetBool("run", isRun);
        anim.SetBool("jump",isLook);
    }
}
