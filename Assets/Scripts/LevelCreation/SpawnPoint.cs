using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject character;
    public GameObject charChild;


    public bool isBeingDragged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingDragged)
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newPos;
        }
        else
        {
            float xPos = transform.position.x;
            float yPos = transform.position.y;

            xPos = Mathf.RoundToInt(xPos);
            yPos = Mathf.RoundToInt(yPos);

            if(xPos < GlobalData.spawnMinX)
            {
                xPos = GlobalData.spawnMinX;
            }
            else if(xPos > GlobalData.spawnMaxX)
            {
                xPos = GlobalData.spawnMaxX;
            }

            if (yPos < GlobalData.spawnMinY)
            {
                yPos = GlobalData.spawnMinY;
            }
            else if (yPos > GlobalData.spawnMaxY)
            {
                yPos = GlobalData.spawnMaxY;
            }

            Vector2 newPos = new Vector2(xPos, yPos);

            transform.position = newPos;
        }
        transform.position += Vector3.forward;  //lazy way of making the spawns not overlap the players
    }

    public void MoveCharacterToMe()
    {
        character.transform.position = (Vector2) transform.position;
        charChild.transform.position = (Vector2) transform.position;
        charChild.SetActive(true);
    }

}
