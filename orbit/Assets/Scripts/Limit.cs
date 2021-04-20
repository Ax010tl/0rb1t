/*
If the rocket exceeds the game limits, you lose a life and return 
to level start. 
Valeria Pineda
10/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Limit : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject endMessage;
    [SerializeField] Text endText;
    [SerializeField] GameObject levelFade;
    [SerializeField] Text gravity;
    LevelFader fader;
    Manager sct;
    GameObject rocket;
    AudioManager sfx;

    // Start is called before the first frame update
    void Start()
    {
        sct = canvas.GetComponent<Manager>();
        rocket = GameObject.FindWithTag("Player");
        fader = levelFade.GetComponent<LevelFader>();
        sfx = canvas.GetComponent<AudioManager>();
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player")
        {
            manageCollision();
        }
    }

    public void manageCollision() {
        // Return rocket to start position
        rocket.transform.position = new Vector3(-0.03000032f, -3.17f, 0);
        rocket.transform.rotation = Quaternion.Euler(0, 0, -42.381f);
        rocket.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        rocket.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if (PlayerPrefs.GetInt("lives") > 1) {
            // Subtract life
            sct.changeLives(-1);

            // Show message to try again
            endText.text = "Intenta de nuevo :(";
            endMessage.SetActive(true);
            StartCoroutine(delayRestart());
        }
        else {
            PlayerPrefs.SetInt("lives", 0);
            sfx.gameOverSound();
            // Show end of game message
            endText.text = "¡Ya no tienes vidas!";
            endMessage.SetActive(true);
            StartCoroutine(delayEnd());
        }
    }

    IEnumerator delayRestart() {
        yield return new WaitForSeconds(1.8f);
        endMessage.SetActive(false);
        gravity.enabled = true;

        // Be able to throw it once more
        rocket.GetComponent<Launch>().canDrag = true;
        rocket.GetComponent<Goal>().numberOfRevolutions = 0;

        yield return new WaitForSeconds(5);
        gravity.enabled = true;
    }

    public IEnumerator delayEnd() {
        yield return new WaitForSeconds(4.0f);
        fader.goToScene("End");
    }

    public void exitGame() 
    {
        sfx.gameOverSound();
        fader.goToScene("End");
        // So fun fact isn't repeated
    }
}
