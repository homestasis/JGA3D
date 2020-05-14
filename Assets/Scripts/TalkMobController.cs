using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TalkMobController : MonoBehaviour
{
    [SerializeField] private float zMoov;
    [SerializeField] private float speed;
    [SerializeField] private float standVec;//止まってる時の向き

    private Rigidbody rb;
    private Animator anim;
    private bool isRun = false;
    private float startPosZ;
    private float nextPosZ;
    private float pos;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.localRotation = Quaternion.Euler(0, standVec, 0);
        startPosZ = transform.position.z;
        nextPosZ = transform.position.z + zMoov;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
        pos = transform.position.z;
        if(zMoov > 0 && pos > nextPosZ)
        {
            isRun = false;
            transform.localRotation = Quaternion.Euler(0, standVec, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, nextPosZ);
        }
        else if(zMoov > 0 && pos < startPosZ)
        {
            isRun = false;
            transform.localRotation = Quaternion.Euler(0, standVec, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosZ);
        }
        else if(zMoov < 0 && pos < nextPosZ) 
        {
            isRun = false;
            transform.localRotation = Quaternion.Euler(0, standVec, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, nextPosZ);
        }
        else if(zMoov < 0 && pos > startPosZ)
        {
            isRun = false;
            transform.localRotation = Quaternion.Euler(0, standVec, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, nextPosZ);
        }

    }

    internal async void MobGo()
    {
        isRun = true;
        if (zMoov > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector3(0, speed, 0);
        }
        else if (zMoov < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector3(0, -speed, 0);
        }
    }

    internal async void MobBack()
    {
        isRun = true;
        if (zMoov > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rb.velocity = new Vector3(0, -speed, 0);
        }
        else if (zMoov < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rb.velocity = new Vector3(0, speed, 0);
        }
    }

    private void SetAnimation()
    {
        anim.SetBool("run", isRun);
    }
}
