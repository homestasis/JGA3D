using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    private bool firstPush = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PressStart();
        }
    }

    public void PressStart()
    {
        if (!firstPush)
        {
            Debug.Log("start");
            SceneManager.LoadScene("FirstStage");
            firstPush = true;
        }
    }
}
