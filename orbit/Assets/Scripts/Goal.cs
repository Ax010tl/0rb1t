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
    [SerializeField] Text endText;
    [SerializeField] Text changeText;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject endMessage;
    [SerializeField] GameObject changeMessage;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject moon;
    [SerializeField] LevelFader fader;
    [SerializeField] FunFactReader ff;
    [SerializeField] Limit limit;
    Manager sct;
    Launch launcher;
    int level;

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level");

        // Restart number of revolutions
        numberOfRevolutions = 0;
        // Call Manager.cs and displayAll to show all stats
        sct = canvas.GetComponent<Manager>();
        sct.displayAll();
        // Hide end message
        endMessage.SetActive(false);
        changeMessage.SetActive(false);
        launcher = GetComponent<Launch>();

        // Message to let the users know gravity increases with each level
        if(level == 2) {
            changeText.text = "La gravedad va a ir cambiando.";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }
        // Message to let the users know there will be obstacles from that level on
        else if(level == 5) {
            changeText.text = "Cuidado con los asteroides.";
            changeMessage.SetActive(true);
            StartCoroutine(disappear());
        }

        ff.scoreDelta = 0;

        // Shows obstacle from beginning of scene if moon is disabled
        if(PlayerPrefs.GetInt("level") >= 5 && !moon.activeInHierarchy) {
            obstacle.SetActive(true);
        }
    }

    IEnumerator disappear()
    {
        // Disappear messages after 1 second
        yield return new WaitForSeconds(3); 
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
            // If number of revolutions has been reached or rocket hasn't launched, it won't be counted if player loses lives
            if(numberOfRevolutions >= minRevolutions || launcher.canDrag) {return;}
            else {
                limit.manageCollision();
            }
        }

        // If the rocket collides with an obstacle, points are subtracted
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
        if(level < 13) {
            return "FunFact";
        }
        else {
            return "Level1";
        }
    }

}