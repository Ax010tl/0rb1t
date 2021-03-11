/*
Each method is a button's behaviour. All need to be public so button
inspector can access them.
* Application.Quit won't work while in th editor.
Valeria Pineda
11/03/2021
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void exitGame() 
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("End");
        // Application.Quit();
    }

    public void nextLevel()
    {
        SceneManager.LoadScene("Level1");
    }
}
