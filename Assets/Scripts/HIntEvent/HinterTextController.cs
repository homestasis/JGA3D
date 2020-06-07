using UnityEngine;
using UnityEngine.UI;


public class HinterTextController : MonoBehaviour
{
    private string[] scriptsForPc;
    private string[] scriptsForPad;
    private Text tex;


    private void Awake()
    {
        tex = GetComponent<Text>();

        scriptsForPc = new string[5];
        scriptsForPc[0] = "Enter で 雨を強くできる";
        scriptsForPc[1] = "もう1回 Enter を押して雨を弱くできる";
        scriptsForPc[2] = "雨が強くなると動きが鈍くなる";
        scriptsForPc[3] = "体温を回復できる場所もある";
        scriptsForPc[4] = "体温が0になると動けなくなる";

        scriptsForPad = new string[5];
        scriptsForPad[0] = "Y で 雨を強くできる";
        scriptsForPad[1] = "もう1回 Y を押して雨を弱くできる";
        scriptsForPad[2] = "雨が強くなると動きが鈍くなる";
        scriptsForPad[3] = "体温を回復できる場所もある";
        scriptsForPad[4] = "体温が0になると動けなくなる";

    }

    internal void UpdateText(int i)
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            tex.text = scriptsForPc[i];
        }
        else
        {
            tex.text = scriptsForPad[i];
        }
        
    }
}
