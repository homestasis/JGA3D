using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackScreenController : MonoBehaviour
{
    public float offsetX;
    public float offsetY;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        updatePos();
    }

    // Update is called once per frame
    void Update()
    {
        updatePos();
    }

    public void updatePos()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX,
            player.transform.position.y + offsetY, 0);
    }
}
