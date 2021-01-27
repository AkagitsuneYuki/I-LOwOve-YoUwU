using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParent : MonoBehaviour
{
    //the sprite, either owo or uwu
    [SerializeField] private Transform child;

    #region Raycast
    //raycast stuff, used to check for adjacent objects to see where we can move
    public float rayLength;
    private RaycastHit2D rayHit;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask trapLayerMask;
    [SerializeField] private LayerMask objectsMask;

    //private Vector3[] rayDirection = new Vector3[4];
    private Vector3[] rayDirection = {Vector3.up, Vector3.down, Vector3.left, Vector3.right};

    #endregion

    //tells if we use wasd or arrows as our main controller
    enum MovementType
    {
        Primary,    //owo
        Secondary   //uwu
    }
    [SerializeField] private MovementType movementType;

    //a simple struct used to set and get the directions we can move to
    [System.Serializable]
    public struct Directions
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
    public Directions dir;

    //a simple struct that's used to get what types of objects we're adjecent to
    //this is for when we need to check which directions we can move to
    [System.Serializable]
    public struct AdjacentObject
    {
        //the types of objects that can be adjacent to us, more types will be added as more objects are made
        public enum ObjectType
        {
            none,
            wall,
            player      //use this to prevent the characters from overlapping
        }
        public ObjectType up;
        public ObjectType down;
        public ObjectType left;
        public ObjectType right;
    }
    public AdjacentObject adjObj;

    void Start()
    {
        float x = Mathf.FloorToInt(transform.position.x);
        float y = Mathf.FloorToInt(transform.position.y);
        Vector3 pos = new Vector3(x, y, 0);
        transform.position = pos;

        /*rayDirection[0] = Vector3.up;
        rayDirection[1] = Vector3.down;
        rayDirection[2] = Vector3.left;
        rayDirection[3] = Vector3.right;*/
    }


    void Update()
    {
        CheckAdjacentObjects();
        
        if (child.position.x == transform.position.x)
        {
            if(child.position.y == transform.position.y)
            {
                Movement();
            }
        }
        
    }

    private void Movement()
    {
        switch (movementType)
        {
            //owo
            case MovementType.Primary:
                //up
                if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow)) && dir.up)
                {
                    transform.position += Vector3.up;
                    break;
                }
                //down
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow)) && dir.down)
                {
                    transform.position -= Vector3.up;
                    break;
                }
                //left
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow)) && dir.left)
                {
                    transform.position += Vector3.left;
                    break;
                }
                //right
                if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)) && dir.right)
                {
                    transform.position -= Vector3.left;
                    break;
                }
                break;
            //uwu
            case MovementType.Secondary:
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow)) && dir.down)
                {
                    transform.position -= Vector3.up;
                    break;
                }
                if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow)) && dir.up)
                {
                    transform.position += Vector3.up;
                    break;
                }
                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow)) && dir.right)
                {
                    transform.position -= Vector3.left;
                    break;
                }
                if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow)) && dir.left)
                {
                    transform.position += Vector3.left;
                    break;
                }
                break;
        }
    }


    private void CheckAdjacentObjects()
    {
        Vector3 rd = Vector3.zero;
        for (int i = 0; i < 4; i++)
        {
            rd = rayDirection[i];

            rayHit = Physics2D.Raycast(transform.position, rd, rayLength, objectsMask);
            if(rayHit.collider != null)
            {
                GameObject obj = rayHit.collider.gameObject;
                SetAdjacentObject(i, LayerMask.LayerToName(obj.layer));
            }
            else
            {
                SetAdjacentObject(i, "");
#if UNITY_EDITOR
                Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.blue);
#endif
            }
#if UNITY_EDITOR
            switch (i)
            {
                case (0):
                    if (dir.up)
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.blue);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.red);
                    }
                    break;
                case (1):
                    if (dir.down)
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.blue);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.red);
                    }
                    break;
                case (2):
                    if (dir.left)
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.blue);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.red);
                    }
                    break;
                case (3):
                    if (dir.right)
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.blue);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, transform.position + rayDirection[i] * rayLength, Color.red);
                    }
                    break;
            }
#endif
        }
    }

    //this could probably be done better
    private void SetAdjacentObject(int side, string layerName)
    {
        switch (side)
        {
            default:
                print("invalid side index " + side);
                break;
            case (0):
                switch (layerName)
                {
                    default:
                        adjObj.up = AdjacentObject.ObjectType.none;
                        dir.up = true;
                        break;
                    case ("Wall"):
                        adjObj.up = AdjacentObject.ObjectType.wall;
                        dir.up = false;
                        break;
                    case ("OwOParent"):
                        adjObj.up = AdjacentObject.ObjectType.player;
                        dir.up = false;
                        break;
                    case ("UwUParent"):
                        adjObj.up = AdjacentObject.ObjectType.player;
                        dir.up = false;
                        break;
                }
                break;
            case (1):
                switch (layerName)
                {
                    default:
                        adjObj.down = AdjacentObject.ObjectType.none;
                        dir.down = true;
                        break;
                    case ("Wall"):
                        adjObj.down = AdjacentObject.ObjectType.wall;
                        dir.down = false;
                        break;
                    case ("OwOParent"):
                        adjObj.down = AdjacentObject.ObjectType.player;
                        dir.down = false;
                        break;
                    case ("UwUParent"):
                        adjObj.down = AdjacentObject.ObjectType.player;
                        dir.down = false;
                        break;
                }
                break;
            case (2):
                switch (layerName)
                {
                    default:
                        adjObj.left = AdjacentObject.ObjectType.none;
                        dir.left = true;
                        break;
                    case ("Wall"):
                        adjObj.left = AdjacentObject.ObjectType.wall;
                        dir.left = false;
                        break;
                    case ("OwOParent"):
                        adjObj.left = AdjacentObject.ObjectType.player;
                        dir.left = false;
                        break;
                    case ("UwUParent"):
                        adjObj.left = AdjacentObject.ObjectType.player;
                        dir.left = false;
                        break;
                }
                break;
            case (3):
                switch (layerName)
                {
                    default:
                        adjObj.right = AdjacentObject.ObjectType.none;
                        dir.right = true;
                        break;
                    case ("Wall"):
                        adjObj.right = AdjacentObject.ObjectType.wall;
                        dir.right = false;
                        break;
                    case ("OwOParent"):
                        adjObj.right = AdjacentObject.ObjectType.player;
                        dir.right = false;
                        break;
                    case ("UwUParent"):
                        adjObj.right = AdjacentObject.ObjectType.player;
                        dir.right = false;
                        break;
                }
                break;
        }
    }

}
