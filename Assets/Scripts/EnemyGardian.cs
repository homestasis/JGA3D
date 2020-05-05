using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyGardian : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float startPosX = 0;
    [SerializeField] private float nextPosX = 0;
    [SerializeField] private float speed = 0;

    private Rigidbody rb = null;
    private Animator anim = null;
    private bool isRun = false;
    private float vec;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        vec = nextPosX - startPosX;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("RunAway");
        }
    }

    IEnumerator RunAway()
    {
        if (vec < 0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            rb.velocity = new Vector3(-speed, 0, 0);
            isRun = true;
            anim.SetBool("run", isRun);
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
    
        }
        else if (vec > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            rb.velocity = new Vector3(speed, 0, 0);
            isRun = true;
            anim.SetBool("run", isRun);
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
}
