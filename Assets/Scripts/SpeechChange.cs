using System.Collections;
using UnityEngine;

public class SpeechChange : MonoBehaviour
{
    private MeshRenderer mesh;
    private bool isDisplay;
    private static Material[] mates;

    // Start is called before the first frame update
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        if(mates == null)
        {
            mates = new Material[2];
           
            if(Input.GetJoystickNames().Length == 0)
            {
                mates[0] = Resources.Load<Material>("speech_key1");
                mates[1] = Resources.Load<Material>("speech_key2");
            }
            else
            {
                //コントローラー用の画像のロード
            }
        }
    }

    internal IEnumerator SetImage()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = true;
        int i = 0;
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
