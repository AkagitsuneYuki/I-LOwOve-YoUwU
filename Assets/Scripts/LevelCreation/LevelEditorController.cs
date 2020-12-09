using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelEditorController : MonoBehaviour
{
    public WallButton.WallType[] walls;

    public enum CursorMode
    {
        None,   //don't add or subtract anything
        Erase,
        Wall,
        Players, //for when setting the spawn points
        Trap
    }
    public CursorMode mode;
    public SpawnPoint[] spawns;
    public bool editMode;

    void Start()
    {
        editMode = true;

        GameObject[] obj = GameObject.FindGameObjectsWithTag("Editor Wall");

        walls = new WallButton.WallType[obj.Length];

        for(int i = 0; i < obj.Length; i++)
        {
            try
            {
                walls[i] = obj[i].GetComponent<WallButton>().wallType;
            }
            finally
            {

            }
        }
    }

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
                    //Debug.Log("Something was clicked!");
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
                            else if(mode == CursorMode.Trap)
                            {
                                wall.SetWallType(WallButton.WallType.trap);
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
            else if (Input.GetKeyDown(KeyCode.T))
            {
                mode = CursorMode.Trap;
            }
        }
    }

    //saving works
    public void SaveLevel()
    {
        string dest = Application.persistentDataPath + "/test.uwu";
        FileStream file;

        if (File.Exists(dest))
        {
            file = File.OpenWrite(dest);
        }
        else
        {
            file = File.Create(dest);
        }
        BinaryFormatter bf = new BinaryFormatter();
        LevelData data = new LevelData(walls);
        bf.Serialize(file, data);
        file.Close();
    }
    //loading is a bit fucked atm
    public void LoadLevel()
    {
        string dest = Application.persistentDataPath + "/test.uwu";
        FileStream file;

        if (File.Exists(dest))
        {
            file = File.OpenRead(dest);
        }
        else
        {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        LevelData data = (LevelData)bf.Deserialize(file);
        file.Close();

        walls = data.buttons;

    }
}
