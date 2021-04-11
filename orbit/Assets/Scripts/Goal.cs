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
    public int numberOfRevolutions; 
    //in order to make it more difficult, we are sttaing a min number of orbits the rocket must do 
    [SerializeField] int minRevolutions;
    public GameObject canvas;
    Manager sct;
    public GameObject endMessage;

    // Start is called before the first frame update
    void Start()
    {
        // Restart number of revolutions
        numberOfRevolutions = 0;
        // Call Manager.cs and displayAll to show all stats
        sct = canvas.GetComponent<Manager>();
        sct.displayAll();
        // Hide end message
        endMessage.SetActive(false);
    }
    
    //If the player is out of lives, it's game over
    private void HandleLives ()
    {
        if(PlayerPrefs.GetInt("lives") <= 0){
            SceneCoroutine();
            SceneManager.LoadScene("End");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        // Add points to score if rocket collides with a powerup
        if(collider.tag == "PowerUp"){
            sct.changeScore(1);
        }

        // We need to add points if it manages the minimum complete orbits
        if(collider.tag == "Limit"){
            numberOfRevolutions++;
            if(numberOfRevolutions == minRevolutions){
                // Change levels and scores
                sct.changeScore(1);
                // Show achievement message
                endMessage.SetActive(true);
                // Wait for 5 seconds before showing next scene
                StartCoroutine(SceneCoroutine());
            }
        }

        // If the rocket collides with the planet, lives are subtracted
        if(collider.tag == "Planet") {
            // Once number of revolutions has been reachedd, it won't be counted if player loses lives
            if(numberOfRevolutions == minRevolutions) {}
            else {
                sct.changeLives(-1);
                HandleLives();
            }
        }
    }

    // Wait a few seconds before new scene and level
    IEnumerator SceneCoroutine()
    {
        yield return new WaitForSeconds(5);
        sct.changeLevel(1);
        SceneManager.LoadScene( getFunFactScene() );
    }

    // Go to fun fact depending on power up sprite
    string getFunFactScene(){
        GameObject powerUp = GameObject.FindWithTag("PowerUp");
        string powerUpType = powerUp.GetComponent<SpriteRenderer>().sprite.name;
        print(powerUpType);
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



