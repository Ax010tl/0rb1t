/*
If the rocket exceeds the game limits, you lose a life. 
Valeria Pineda
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Limit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player")
        {
            // Subtract life
            print("-1 vida :(");
            PlayerPrefs.SetInt("lives", PlayerPrefs.GetInt("lives")-1);
            print(PlayerPrefs.GetInt("lives"));

            // Return rocket to start position
            GameObject rocket = GameObject.FindWithTag("Player");
            rocket.transform.position = new Vector3(-0.03000032f, -3.17f, 0);
            rocket.transform.rotation = Quaternion.Euler(0, 0, -42.381f);

            // SceneManager.LoadScene("Level1");
        }
    }
}
