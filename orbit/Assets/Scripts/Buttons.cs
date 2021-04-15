/*
Each method is a button's behaviour. All need to be public so button
inspector can access them.
Valeria Pineda
11/03/2021
*/

using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public string nextScene, subject;
    public void exitGame() 
    {
        Debug.Log("Exit Game");
        SceneManager.LoadScene("End");
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
