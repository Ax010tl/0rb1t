/*
This class handles the basics of the game: score and lives. 
Lourdes Badillo & Eduardo Villalpando
9/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] Text gravityText;
    int G; 
    [SerializeField] GameObject bounds;
    Manager sct;
    Limit limit;

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

        // An obstacle will be activated once the player reaches level 5
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacle = obstacles[0];
        obstacle.SetActive(false);

        if(PlayerPrefs.GetInt("level") == 2){
            changeText.text = "La gravedad ha aumentado";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }

        if(PlayerPrefs.GetInt("level") == 5){
            changeText.text = "Cuidado con los asteroides";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }

        G = PlayerPrefs.GetInt("level") + 7;
        gravityText.text = "Gravedad: " + G.ToString(); 
        StartCoroutine(disappearGravity());
    }

    void Update() {
        sct.displayAll();

        if(PlayerPrefs.GetInt("level") >= 5){
            obstacle.SetActive(true);
        }
        else{
            obstacle.SetActive(false);
        }

    }

    IEnumerator disappear ()
    {
       yield return new WaitForSeconds(1); 
       changeMessage.SetActive(false);
    }

    IEnumerator disappearGravity ()
    {
       yield return new WaitForSeconds(2); 
       gravityText.enabled = false;
    }

    //If the player is out of lives, it's game over
    private void HandleLives ()
    {
        if(PlayerPrefs.GetInt("lives") <= 0){
            // Show end of game message
            endText.text = "¡Ya no tienes vidas!";
            endMessage.SetActive(true);
            StartCoroutine(limit.delayEnd());
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
                endText.text = "¡Nivel completado!";
                endMessage.SetActive(true);
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
        yield return new WaitForSeconds(5);
        sct.changeLevel(1);
        SceneManager.LoadScene( getFunFactScene() );
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



