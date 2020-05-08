using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechChange : MonoBehaviour
{
    [SerializeField] private Material[] mates;
    private MeshRenderer mesh;
    private bool isDisplay;


    // Start is called before the first frame update
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
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
}
