/*
This class handles the basics of the game: score and lives. 
Lourdes Badillo & Eduardo Villalpando
9/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public int numberOfRevolutions; 
    //in order to make it more difficult, we are setting a min number of orbits the rocket must do 
    [SerializeField] int minRevolutions;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject endMessage;
    [SerializeField] Text endText;
    [SerializeField] GameObject changeMessage;
    [SerializeField] Text changeText;
    [SerializeField] GameObject bounds;
    [SerializeField] GameObject levelFade;
    LevelFader fader;
    Manager sct;
    Limit limit;
    FunFactReader ff;

    GameObject[] obstacles;
    GameObject obstacle; 
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
        changeMessage.SetActive(false);
        // Call Limit.cs to handle collision with planet
        limit = bounds.GetComponent<Limit>();
        // Call LevelFader.cs to fade in and out of level
        fader = levelFade.GetComponent<LevelFader>();
        levelFade.SetActive(true);

        // An obstacle will be activated once the player reaches level 5
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacle = obstacles[0];
        obstacle.SetActive(false);

        // Message to let the users know gravity increases with each level
        if(PlayerPrefs.GetInt("level") == 2){
            changeText.text = "La gravedad va a ir cambiando.";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }

        // Message to let the users know there will be obstacles from that level on
        if(PlayerPrefs.GetInt("level") == 5){
            changeText.text = "Cuidado con los asteroides.";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }

        ff = GameObject.FindWithTag("FunFact").GetComponent<FunFactReader>();
        ff.scoreDelta = 0;
    }

    void Update() {
        sct.displayAll();

        // Asteroids will show up on level 5
        if(PlayerPrefs.GetInt("level") >= 5){
            obstacle.SetActive(true);
        }
        else{
            obstacle.SetActive(false);
        }

    }

    IEnumerator disappear()
    {
        // Disappear messages after 1 second
        yield return new WaitForSeconds(5); 
        changeMessage.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider){
        // Add points to score if rocket collides with a powerup
        if(collider.tag == "PowerUp"){
            // Once number of revolutions has been reached, it won't be counted if player gets powerup
            if(numberOfRevolutions >= minRevolutions) {return;}
            else {
                sct.changeScore(1);
                ff.scoreDelta++;
            }
        }

        // We need to add points if it manages the minimum complete orbits
        if(collider.tag == "Limit"){
            numberOfRevolutions++;
            if(numberOfRevolutions == minRevolutions){
                // Change levels and scores
                sct.changeScore(1);
                // Wait for 5 seconds before showing next scene
                StartCoroutine(SceneCoroutine());
            }
        }

        // If the rocket collides with the planet, lives are subtracted
        if(collider.tag == "Planet") {
            // Once number of revolutions has been reached, it won't be counted if player loses lives
            if(numberOfRevolutions >= minRevolutions) {return;}
            else {
                limit.manageCollision();
            }
        }

        // If the rocket collides with an obstacle, lives(?) are subtracted
        if(collider.tag == "Obstacle") {
            // Once number of revolutions has been reached, it won't be counted if player collides with obstacles
            if(numberOfRevolutions >= minRevolutions) {return;}
            else {
                sct.changeScore(-1);
            }
        }
    }

    // Wait a few seconds before new scene and level
    IEnumerator SceneCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        sct.changeLevel(1);
        // Show achievement message
        endText.text = "¡Nivel completado!";
        endMessage.SetActive(true);
        
        yield return new WaitForSeconds(1);
        fader.goToScene( getFunFactScene() );
    }

    // Go to fun fact depending on power up sprite and facts that have already been displayed
    string getFunFactScene(){
        string powerUpType = GameObject.FindWithTag("PowerUp").GetComponent<SpriteRenderer>().sprite.name;

        if(ff.factsExist(powerUpType)) {
            return "FunFact";
        }
        else {
            return "Level1";
        }
    }

}



