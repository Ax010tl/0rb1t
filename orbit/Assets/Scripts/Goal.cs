using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] int numberOfRevolutions;
    [SerializeField] int minRevolutions;
    // Start is called before the first frame update
    void Start()
    {
        numberOfRevolutions = 0;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.tag == "Player"){
            numberOfRevolutions++;
            if(numberOfRevolutions == minRevolutions){
                SceneManager.LoadScene("FunFact");
            }

        }
    }

}
