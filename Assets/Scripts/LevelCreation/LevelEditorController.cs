using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelEditorController : MonoBehaviour
{
    public WallManager wallManager;
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
                    switch (hit.collider.gameObject.tag)
                    {
                        default:
                            //print("do something");
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
        //GameObject[] obj = GameObject.FindGameObjectsWithTag("Editor Wall");
        GameObject[] obj = new GameObject[wallManager.rows.Length + wallManager.cols.Length];
        int temp = 0;
        while (temp < obj.Length)
        {
            for(int i = 0; i < wallManager.rows.Length; i++)
            {
                obj[temp] = wallManager.rows[i];
                temp++;
            }
            for (int i = 0; i < wallManager.cols.Length; i++)
            {
                obj[temp] = wallManager.cols[i];
                temp++;
            }
        }

        walls = new WallButton.WallType[obj.Length];

        for (int i = 0; i < obj.Length; i++)
        {
            try
            {
                Debug.Log(obj[i].name + " at " + obj[i].transform.position);
                walls[i] = obj[i].GetComponent<WallButton>().wallType;
            }
            finally
            {
            }
        }

        //this is only temporary
        string dest = Application.persistentDataPath + "/test.uwu";
        //FileStream file;

        BinaryWriter writer = new BinaryWriter(File.Open(dest, FileMode.Create));

        //this is used to check if the file type is correct
        writer.Write("UwU");

        //the save version number
        const float version = 1.0f;

        writer.Write(version);

        //this is used as a decryption key to make sure the file is not corrupt
        int key = Mathf.FloorToInt(Random.Range(int.MinValue, int.MaxValue));

        writer.Write(key);

        writer.Write(walls.Length); //used when getting the walls when loading

        //this checksum is very basic, it just adds up the wall types as integers
        int checksumA = 0;
        for(int i = 0; i < walls.Length; i++)
        {
            writer.Write((byte)walls[i]);
            checksumA += (byte)walls[i];
        }
        //this is used to see if the data is corrupt. it's the checksum xor'd with the key
        //if we xor this with the key a second time it would return the checksum
        writer.Write(checksumA ^ key);

        //the player locations
        //owo
        writer.Write("OwO");
        int owoX = Mathf.FloorToInt(spawns[0].gameObject.transform.position.x);
        int owoY = Mathf.FloorToInt(spawns[0].gameObject.transform.position.y);
        writer.Write(owoX);
        writer.Write(owoY);
        writer.Write((owoX + owoY) ^ key);

        //uwu
        writer.Write("UwU");
        int uwuX = Mathf.FloorToInt(spawns[1].gameObject.transform.position.x);
        int uwuY = Mathf.FloorToInt(spawns[1].gameObject.transform.position.y);
        writer.Write(uwuX);
        writer.Write(uwuY);
        writer.Write((uwuX + uwuY) ^ key);
    }

    public void LoadLevel()
    {
        string dest = Application.persistentDataPath + "/test.uwu";

        if (File.Exists(dest))
        {
            //file = File.OpenRead(dest);
            using (BinaryReader reader = new BinaryReader(File.Open(dest, FileMode.Open)))
            {
                //test if we begin with the UwU string
                if(reader.ReadString() == "UwU")
                {
                    //get the file version number
                    float version = reader.ReadSingle();
                    switch (version)
                    {
                        //when we don't recognize this file's version
                        default:
#if UNITY_EDITOR
                            Debug.Log("unrecognized version");
#endif
                            break;
                        //v1.0
                        case (1.0f):
                            ReadFileVersionOne(reader);
                            break;
                    }
                }
#if UNITY_EDITOR
                else
                {
                    Debug.Log("This file has an invalid header");
                }
#endif
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogError("File not found");
#endif
            return;
        }
    }

    private void ReadFileVersionOne(BinaryReader reader)
    {
        //get the file's encryption key
        int key = reader.ReadInt32();

        //get the amount of bytes to read
        int len = reader.ReadInt32();

        GameObject[] obj = GameObject.FindGameObjectsWithTag("Editor Wall");
        //we get a backup of the original walls
        walls = new WallButton.WallType[obj.Length];
        for (int i = 0; i < obj.Length; i++)
        {
            try
            {
                walls[i] = obj[i].GetComponent<WallButton>().wallType;
            }
            finally
            {
            }
        }

        if (len == obj.Length)
        {
            int checksumA = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                try
                {
                    byte num = reader.ReadByte();
                    obj[i].GetComponent<WallButton>().SetWallType((WallButton.WallType)num);
                    checksumA += num;
                }
                finally
                {
                }
            }
            int testCheck = reader.ReadInt32(); //the encrypted checksum
            testCheck ^= key;   //decrypt it
            if(checksumA == testCheck)
            {
#if UNITY_EDITOR
                Debug.Log("Wall data passed the checksum");
#endif
                string test = reader.ReadString();
                if(test == "OwO")
                {
                    Vector3[] oldSpawns = {spawns[0].gameObject.transform.position, spawns[1].gameObject.transform.position};
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    int num = x + y;
                    int check = reader.ReadInt32();
                    if((check ^ key) == num)
                    {
                        spawns[0].gameObject.transform.position = new Vector2(x, y);
                        test = reader.ReadString();
                        if(test == "UwU")
                        {
                            x = reader.ReadInt32();
                            y = reader.ReadInt32();
                            num = x + y;
                            check = reader.ReadInt32();
                            if ((check ^ key) == num)
                            {
                                spawns[1].gameObject.transform.position = new Vector2(x, y);
                            }
                            else
                            {
                                spawns[0].transform.position = oldSpawns[0];
                                spawns[1].transform.position = oldSpawns[1];
                                ResetToLastSave(obj);
                            }
                        }
                        else
                        {
                            spawns[0].transform.position = oldSpawns[0];
                            ResetToLastSave(obj);
                        }
                    }
                    else
                    {
                        ResetToLastSave(obj);
                    }
                }
                else
                {
                    ResetToLastSave(obj);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("The wall data failed the checksum, is the file corrupt?");
#endif
                ResetToLastSave(obj);
            }
        }
    }

    private void ResetToLastSave(GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            try
            {
                obj[i].GetComponent<WallButton>().SetWallType(walls[i]);
            }
            finally
            {
            }
        }
    }
}
