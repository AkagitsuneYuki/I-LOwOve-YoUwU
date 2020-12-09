using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    [SerializeField] private GameObject[] levelPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        //GameObject.Instantiate(levelPrefabs[level], Vector3.zero, Quaternion.identity);
    }

    public void ResetLevel()
    {
        try
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("Edit Spawn");
            spawns[0].GetComponent<SpawnPoint>().MoveCharacterToMe();
            spawns[1].GetComponent<SpawnPoint>().MoveCharacterToMe();
        }
        finally
        {
            Debug.Log("Mike fucked up this one. please nag him on github");
        }

    }

    public void PlayerDied()
    {
        Debug.Log("the player hit something that killed them, reset their positions");
        StartCoroutine(ResetCoroutine());
    }

    IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        ResetLevel();
    }

}
