/*
If the rocket exceeds the game limits, you lose a life and return 
to level start. 
Valeria Pineda
10/04/2021
*/

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Limit : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject endMessage;
    [SerializeField] Text endText;
    Manager sct;
    GameObject rocket;
    // Start is called before the first frame update
    void Start()
    {
        sct = canvas.GetComponent<Manager>();
        rocket = GameObject.FindWithTag("Player");
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player")
        {
            manageCollision();
        }
    }

    public void manageCollision() {
        // Subtract life
        sct.changeLives(-1);

        // Return rocket to start position
        rocket.transform.position = new Vector3(-0.03000032f, -3.17f, 0);
        rocket.transform.rotation = Quaternion.Euler(0, 0, -42.381f);
        rocket.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        rocket.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if (PlayerPrefs.GetInt("lives") > 0) {
            // Show message to try again
            endText.text = "Intenta de nuevo :(";
            endMessage.SetActive(true);
            StartCoroutine(delayRestart());
        }
        else {
            // Show end of game message
            endText.text = "¡Ya no tienes vidas!";
            endMessage.SetActive(true);
            StartCoroutine(delayEnd());
        }
    }

    IEnumerator delayRestart() {
        yield return new WaitForSeconds(1.8f);
        endMessage.SetActive(false);

        // Be able to throw it once more
        rocket.GetComponent<Launch>().canDrag = true;
        rocket.GetComponent<Goal>().numberOfRevolutions = 0;
    }

    public IEnumerator delayEnd() {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("End");
    }
}
