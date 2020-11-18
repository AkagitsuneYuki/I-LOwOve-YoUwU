﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    [System.Serializable]
    public enum WallType
    {
        none,
        wall
    }

    public WallType wallType;

    private Color color;

    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
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
        }
    }

}

