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

}
