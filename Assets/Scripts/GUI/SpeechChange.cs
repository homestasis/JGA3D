using System.Collections;
using UnityEngine;

public class SpeechChange : MonoBehaviour
{
    private MeshRenderer mesh;
    private bool isDisplay;
    private static Material[] matesForPc;
    private static Material[] matesForPad;

    // Start is called before the first frame update
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        if(matesForPc == null)
        {
            matesForPc = new Material[2];
            matesForPad = new Material[2];

            matesForPc[0] = Resources.Load<Material>("speech_key1");
            matesForPc[1] = Resources.Load<Material>("speech_key2");
             
            matesForPad[0] = Resources.Load<Material>("speech_pad1");
            matesForPad[1] = Resources.Load<Material>("speech_pad2");
        }
    }

    internal IEnumerator SetImage()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = true;
        int i = 0;
        Material[] mates = new Material[2];
        if (Input.GetJoystickNames().Length == 0)
        {
            mates = matesForPc;
        }
        else
        {
            mates = matesForPad;
        }
        while (isDisplay)
        {
           // mesh.material = null;
            mesh.material = mates[i];

            i++;
            if (i == 2) { i = 0; }

            yield return new WaitForSeconds(0.3f);
        }
        mesh.enabled = false;
    }

    internal void SetIsDisplay()
    {
        isDisplay = true;
    }
    internal void ResetIsDisplay()
    {
        isDisplay = false;
    }
    internal bool GetIsDisplay()
    {
        return isDisplay;
    }

    internal void Turn(float yRot)
    {
        transform.rotation = Quaternion.Euler(90, yRot, 180);
    }
}
