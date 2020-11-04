using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorController : MonoBehaviour
{

    public enum CursorMode
    {
        None,   //don't add or subtract anything
        Erase,
        Wall
    }

    public CursorMode mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mode != 0)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log("Something was clicked!");
                    switch (hit.collider.gameObject.tag)
                    {
                        default:
                            print("do something");
                            break;
                        case ("Editor Wall"):
                            WallButton wall = hit.collider.gameObject.GetComponent<WallButton>();
                            if(mode == CursorMode.Wall)
                            {
                                wall.SetWallType(WallButton.WallType.wall);
                            }
                            else if(mode == CursorMode.Erase)
                            {
                                wall.SetWallType(WallButton.WallType.none);
                            }
                            break;
                    }
                }
            }
        }
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                mode = CursorMode.Wall;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                mode = CursorMode.Erase;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                mode = 0;
            }
        }
        
    }

}
