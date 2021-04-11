using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("lives", 3);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene( "Level1" );
    }
}
