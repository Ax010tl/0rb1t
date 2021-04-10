/*
Each method is a button's behaviour. All need to be public so button
inspector can access them.
* Application.Quit won't work while in the editor.
Valeria Pineda
11/03/2021
*/

// using System.Collections;
// using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public string nextScene, subject;
    public void exitGame() 
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("End");
        // Application.Quit();
    }

    public void omit()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void interesting()
    {
        PlayerPrefs.SetInt(subject, PlayerPrefs.GetInt(subject)+1);
        SceneManager.LoadScene(nextScene);
    }
}
