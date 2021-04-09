using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    int numberOfRevolutions;
    [SerializeField] int minRevolutions;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    int score;
    int lives; 
    // Start is called before the first frame update
    void Start()
    {
        numberOfRevolutions = 0;
        score = 0; 
        lives = 3; 
        //print starting score
        scoreText.text = "Puntaje: " + score.ToString();
        livesText.text = "Vidas: " + lives.ToString();
    }

    //Keep score
    private void HandleScore ()
    {
        //Change text to new score
        scoreText.text = "Puntaje: " + score.ToString(); 

        if(score < 0){
            SceneManager.LoadScene("End");
        }
    }
    
    private void HandleLives ()
    {
        //Change text to new number
        livesText.text = "Vidas: " + lives.ToString(); 

        if(lives < 0){
            SceneManager.LoadScene("End");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        //We need to add points if it manages two complete orbits
        if(collider.tag == "Limit"){
            numberOfRevolutions++;
            if(numberOfRevolutions == minRevolutions){
                score++;
                HandleScore ();
                SceneManager.LoadScene("FunFact");
            }
        }
        if(collider.tag == "Planet")
        {
            score--;
            lives--;
            HandleScore();
            HandleLives(); 
        }
        if(collider.tag == "PowerUp")
        {
            score++;
            HandleScore();
        }
    }

}
