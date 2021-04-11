/*
If the rocket exceeds the game limits, you lose a life and return 
to level start. 
Valeria Pineda
10/04/2021
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public GameObject canvas;
    Manager sct;
    // Start is called before the first frame update
    void Start()
    {
        sct = canvas.GetComponent<Manager>();
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player")
        {
            // Subtract life
            sct.changeLives(-1);

            // Return rocket to start position
            GameObject rocket = GameObject.FindWithTag("Player");
            rocket.transform.position = new Vector3(-0.03000032f, -3.17f, 0);
            rocket.transform.rotation = Quaternion.Euler(0, 0, -42.381f);

            if(PlayerPrefs.GetInt("lives") > 0){
                // Be able to throw it once more
                rocket.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                rocket.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                rocket.GetComponent<Launch>().canDrag = true;
                rocket.GetComponent<Goal>().numberOfRevolutions = 0;
            }

            else{
                SceneManager.LoadScene("End");
            }
            
        }
    }
}
