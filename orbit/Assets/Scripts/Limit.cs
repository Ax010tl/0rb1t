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
            print("-1 vida :(");
            SceneManager.LoadScene("Level1");
        }
    }
}
