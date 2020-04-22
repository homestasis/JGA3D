using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class wallGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    [Header("Wallを設置する座標")]
    public List<Vector2> wallPoints;

    void Start()
    {
        float width = wallPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        for(int i=0; i<wallPoints.Count(); i++)
        {
            GameObject wall = Instantiate(wallPrefab) as GameObject;
            wall.transform.position = new Vector3(wallPoints[i].x, wallPoints[i].y, 0);
        }
    }
    

}
