/*
This class handles the basics of the game: score and lives. 
Lourdes Badillo & Eduardo Villalpando
9/04/2021
*/
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
        score = PlayerPrefs.GetInt("score"); 
        PlayerPrefs.SetInt("lives", 3);
        lives = PlayerPrefs.GetInt("lives"); 
        //print starting score
        scoreText.text = "Puntaje: " + score.ToString();
        livesText.text = "Vidas: " + lives.ToString();
    }

    // Keep score
    private void HandleScore ()
    {
        //Change text to new score
        scoreText.text = "Puntaje: " + score.ToString(); 

    }
    
    private void HandleLives ()
    {
        // Change text to new number
        livesText.text = "Vidas: " + lives.ToString(); 

        if(lives < 0){
            SceneCoroutine();
            SceneManager.LoadScene("End");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        // Add points to score if rocket collides with a powerup
        if(collider.tag == "PowerUp"){
            powerUping();
        }

        // We need to add points if it manages two complete orbits
        if(collider.tag == "Limit"){
            numberOfRevolutions++;
            if(numberOfRevolutions == minRevolutions){
                powerUping();
                SceneCoroutine();
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level")+1);
                SceneManager.LoadScene( getFunFactScene() );
            }
        }
        if(collider.tag == "Planet") {
            lives--;
            PlayerPrefs.SetInt("lives", lives);
            HandleLives(); 
        }
    }

    
    //wait a few seconds before new scene
    IEnumerator SceneCoroutine()
    {
        yield return new WaitForSeconds(1);
    }

    //Adds points to score and updates label
    void powerUping(){
        score++;
        PlayerPrefs.SetInt("score", score);
        HandleScore ();
    }

    // Go to fun fact depending on power up sprite
    string getFunFactScene(){
        GameObject powerUp = GameObject.FindWithTag("PowerUp");
        string powerUpType = powerUp.GetComponent<SpriteRenderer>().sprite.name;
        switch(powerUpType){
            case "math":
                return "FFMath1";
            case "bio":
                return "FFBio1";
            case "chem":
                return "FFChem1";
            case "phys":
                return "FFPhys1";
            case "eng":
                return "FFEng1";
            case "tech":
                return "FFTech1";
            default:
                return "End";
        }
    }

}



