using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementChild : MonoBehaviour
{

    [SerializeField] private Transform parentTransform;

    //might remove this depending on how i go about this
    [SerializeField] private bool canMove;
    public bool CanMove
    {
        get
        {
            return canMove;
        }
    }

    void Start()
    {
        canMove = true;
    }

    void FixedUpdate()
    {
        MoveToParent();
    }

    private void MoveToParent()
    {

        float newX = (float)Math.Round((transform.position.x + parentTransform.position.x) / 2, 1);

        if (transform.position.x != parentTransform.position.x)
        {
            //check is the absolute difference of my x and parent's x, subtract 0.1, then rounded to 1 decimal point
            //when 0, we round to the nearest int. anyother number will be ignored
            float check = (float)Math.Round(Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(parentTransform.position.x)) - 0.1f, 1);
            if (check == 0)
            {
                print("rounding x to integer");
                newX = Mathf.Round(newX);
            }
        }

        float newY = (float)Math.Round((transform.position.y + parentTransform.position.y) / 2, 1);

        if (transform.position.y != parentTransform.position.y)
        {
            //check is the absolute difference of my y and parent's y, subtract 0.1, then rounded to 1 decimal point
            //when 0, we round to the nearest int. anyother number will be ignored
            float check = (float)Math.Round(Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(parentTransform.position.y)) - 0.1f, 1);
            if (check == 0)
            {
                print("rounding y to integer");
                newY = Mathf.Round(newY);
            }
        }

        transform.position = new Vector3(newX, newY);
    }
}
