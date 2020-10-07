using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParent : MonoBehaviour
{

    [SerializeField] private Transform child;

    public float rayLength;
    private RaycastHit2D rayHit;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask trapLayerMask;

    enum MovementType
    {
        Primary,    //owo
        Secondary   //uwu
    }

    [SerializeField] private MovementType movementType;

    [System.Serializable]
    public class Directions
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }
    public Directions dir;

    void Start()
    {
        float x = Mathf.FloorToInt(transform.position.x);
        float y = Mathf.FloorToInt(transform.position.y);
        Vector3 pos = new Vector3(x, y, 0);
        transform.position = pos;
    }


    void Update()
    {
        DrawLines();
        if(child.position.x == transform.position.x)
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

    private void DrawLines()
    {
        //hit right
        if (Physics2D.Raycast(transform.position, transform.right, rayLength, wallLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.right * rayLength, Color.red);
            rayHit = Physics2D.Raycast(transform.position, Vector2.right, rayLength);
            dir.right = false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.right * rayLength, Color.blue);
            dir.right = true;
            if (Physics2D.Raycast(transform.position, transform.right, rayLength, trapLayerMask)) //detects if there is a red wall at the right
            {
                
            }
            else
            {
                
            }
        }
        //left
        if (Physics2D.Raycast(transform.position, -transform.right, rayLength, wallLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.left * rayLength, Color.red);
            rayHit = Physics2D.Raycast(transform.position, Vector2.left, rayLength);
            dir.left = false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.left * rayLength, Color.blue);
            dir.left = true;

            if (Physics2D.Raycast(transform.position, -transform.right, rayLength, trapLayerMask)) //detects if there is a red wall at the left
            {
                
            }
            else
            {
                
            }
        }
        //up
        if (Physics2D.Raycast(transform.position, transform.up, rayLength, wallLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * rayLength, Color.red);
            rayHit = Physics2D.Raycast(transform.position, Vector2.up, rayLength);
            dir.up = false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.up * rayLength, Color.blue);
            dir.up = true;
            if (Physics2D.Raycast(transform.position, transform.up, rayLength, trapLayerMask)) //detects if there is a red wall at the top
            {
                
            }
            else
            {
                
            }
        }
        //down
        if (Physics2D.Raycast(transform.position, -transform.up, rayLength, wallLayerMask))
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * rayLength, Color.red);
            rayHit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);
            dir.down = false;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * rayLength, Color.blue);
            dir.down = true;
            if (Physics2D.Raycast(transform.position, -transform.up, rayLength, trapLayerMask)) //detects if there is a red wall at the bottom
            {
                
            }
            else
            {

            }
        }
    }

}
