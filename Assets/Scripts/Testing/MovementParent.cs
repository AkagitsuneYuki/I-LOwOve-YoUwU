using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementParent : MonoBehaviour
{

    [SerializeField] private Transform child;

    enum MovementType
    {
        Primary,    //owo
        Secondary   //uwu
    }

    [SerializeField] private MovementType movementType;

    void Start()
    {
        float x = Mathf.FloorToInt(transform.position.x);
        float y = Mathf.FloorToInt(transform.position.y);
        Vector3 pos = new Vector3(x, y, 0);
        transform.position = pos;
    }


    void Update()
    {
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
                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.position += Vector3.up;
                    break;
                }
                //down
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.position -= Vector3.up;
                    break;
                }
                //left
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += Vector3.left;
                    break;
                }
                //right
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position -= Vector3.left;
                    break;
                }
                break;
            //uwu
            case MovementType.Secondary:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    transform.position -= Vector3.up;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.position += Vector3.up;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position -= Vector3.left;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += Vector3.left;
                    break;
                }
                break;
        }
    }

}
