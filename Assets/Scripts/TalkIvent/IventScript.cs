using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IventScript : MonoBehaviour
{
    public TextController textController;
    //CharacterScriptから送られてきたAflagflagの格納用
    bool flag2 = false;
    //CharacterScriptから送られてきたscenariosの格納用
    string[] scenarios2;


    public void StartIvent(bool flag, string[] scenarios)
    {
        flag2 = flag;
        scenarios2 = scenarios;
    }


    public void Talk()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (flag2)
            {
                textController.StartText(scenarios2);
                flag2 = false;
            }
        }
    }
}
