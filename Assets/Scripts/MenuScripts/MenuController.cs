using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int editorId;

    // Start is called before the first frame update
    void Start()
    {
        if(editorId == SceneManager.GetActiveScene().buildIndex)
        {
            editorId++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelEditor()
    {
        SceneManager.LoadScene(editorId);
    }

}
