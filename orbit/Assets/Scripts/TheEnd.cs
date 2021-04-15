/*
Manage behaviour when the game ends. (Send data to DB,
restart game, etc.)
Valeria Pineda
14/04/2021
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TheEnd : MonoBehaviour 
{
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Text highScoreText;
    SendFormData message;

    private void Start() {
        message = GetComponent<SendFormData>();
        StartCoroutine(message.uploadData());

        livesText.text = "Vidas: " + PlayerPrefs.GetInt("lives");
        levelText.text = "Completaste " + PlayerPrefs.GetInt("level") + " nivel" + (PlayerPrefs.GetInt("level") == 1 ? "" : "es");
        scoreText.text = PlayerPrefs.GetInt("score") + " puntos";

        if (PlayerPrefs.GetInt("highscore") < PlayerPrefs.GetInt("score")) {
            PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));

            scoreText.color = new Color(0.9725491f, 0.7764707f, 0.2f);
            highScoreText.color = scoreText.color;
        }    
    }

    private void Update() {
        highScore();
    }

    public void restartGame() {
        PlayerPrefs.SetInt("lives", 1);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

        PlayerPrefs.SetInt("biology", 0);
        PlayerPrefs.SetInt("chemistry", 0);
        PlayerPrefs.SetInt("engineering", 0);
        PlayerPrefs.SetInt("math", 0);
        PlayerPrefs.SetInt("physics", 0);
        PlayerPrefs.SetInt("tech", 0);

        SceneManager.LoadScene("Level1");
    }

    private void highScore() {
        // if (PlayerPrefs.GetInt("highscore") < PlayerPrefs.GetInt("score")) {
        //     PlayerPrefs.SetInt("highScore", PlayerPrefs.GetInt("score"));

        //     scoreText.color = new Color(0.9725491f, 0.7764707f, 0.2f);
        //     highScoreText.color = scoreText.color;
        // }
        highScoreText.text = "Puntuación más alta:\n" + PlayerPrefs.GetInt("highscore");
    }
}