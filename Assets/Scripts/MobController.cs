using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [SerializeField] private float delta;// 常に>0で
    [SerializeField] private float speed;

    private Animator anim;
    private Rigidbody rb;
    private float pos;
    private float min;
    private float max;
    private bool key = true;

    // Start is called before the first frame update
    void Awake()
    {
        pos = transform.position.x;
        min = pos - delta;
        max = pos + delta;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < min)
        {
            key = true;
        }
        else if(transform.position.x > max)
        {
            key = false;
        }

        if (key)
        {
            Right();
        }
        else if (!key)
        {
            Left();
        }

        anim.SetBool("run",true);
    }

    void Right()
    {
        transform.localRotation = Quaternion.Euler(0, 90, 0);
        rb.velocity = new Vector3(speed, 0, 0);
    }

    void Left()
    {
        transform.localRotation = Quaternion.Euler(0, -90, 0);
        rb.velocity = new Vector3(-speed, 0, 0);
    }
}
