using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    [System.Serializable]
    public enum WallType
    {
        none,
        wall,
        trap
    }

    public WallType wallType;

    private Color color;

    public Camera camera;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        color = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        
    }


    public void SetWallType(WallType type)
    {
        wallType = type;
        switch (type)
        {
            default:
                GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.layer = 0;
                break;
            case (WallType.wall):
                GetComponent<SpriteRenderer>().color = Color.black;
                gameObject.layer = GlobalData.wallLayer;
                break;
            case (WallType.trap):
                GetComponent<SpriteRenderer>().color = Color.red;
                gameObject.layer = GlobalData.wallLayer;
                break;
        }
    }

}

