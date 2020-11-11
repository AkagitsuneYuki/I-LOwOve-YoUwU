using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorController : MonoBehaviour
{

    public enum CursorMode
    {
        None,   //don't add or subtract anything
        Erase,
        Wall,
        Players //for when setting the spawn points
    }

    public CursorMode mode;

    public SpawnPoint[] spawns;

    public bool editMode;

    // Start is called before the first frame update
    void Start()
    {
        editMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (editMode)
        {
            Editor();
        }
        else
        {
            SwitchMode();
        }
    }

    private void Editor()
    {
        //if (mode != 0)
        //{
            if (Input.GetMouseButton(0) && mode != 0)
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
                            if (mode == CursorMode.Wall)
                            {
                                wall.SetWallType(WallButton.WallType.wall);
                            }
                            else if (mode == CursorMode.Erase)
                            {
                                wall.SetWallType(WallButton.WallType.none);
                            }
                            break;
                        case ("Edit Spawn"):
                            if(mode == CursorMode.Players)
                            {
                                if (Input.GetMouseButtonDown(0))
                                {
                                    hit.collider.gameObject.GetComponent<SpawnPoint>().isBeingDragged = true;
                                }

                            }
                            break;
                    }
                }
            }
            //else
            //{
                //to do: clean this up
                if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0))
                {
                    if (mode == CursorMode.Players)
                    {
                        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                        if (hit.collider != null)
                        {
                            if(hit.collider.gameObject.tag == "Edit Spawn")
                            {
                                hit.collider.gameObject.GetComponent<SpawnPoint>().isBeingDragged = false;
                            }
                        }
                    }
                //}
                
            }
        //}
        SwitchMode();
    }

    private void SwitchMode()
    {
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
                editMode = !editMode;
                if (!editMode)
                {
                    spawns[0].MoveCharacterToMe();
                    spawns[1].MoveCharacterToMe();
                }
                else
                {
                    spawns[0].charChild.SetActive(false);
                    spawns[1].charChild.SetActive(false);
                }
                mode = 0;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                mode = CursorMode.Players;
            }
        }
    }

}
