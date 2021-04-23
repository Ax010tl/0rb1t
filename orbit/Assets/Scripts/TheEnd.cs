/*
Manage behaviour when the game ends. (Send data to DB,
restart game, etc.)
Valeria Pineda
14/04/2021
*/

using UnityEngine;
using UnityEngine.UI;

public class TheEnd : MonoBehaviour 
{
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text levelText;
    [SerializeField] Text highScoreText;
    [SerializeField] LevelFader fader;
    [SerializeField] FunFactReader ff;
    SendFormData message;

    private void Start() {
        message = GetComponent<SendFormData>();
        StartCoroutine(message.uploadData());

        int level = PlayerPrefs.GetInt("level")-1;

        livesText.text = PlayerPrefs.GetInt("lives") + " vida" + (PlayerPrefs.GetInt("lives") == 1? "" : "s");
        levelText.text = "Completaste " + level + " nivel" + (level == 1 ? "" : "es");
        scoreText.text = PlayerPrefs.GetInt("score") + " punto" + (PlayerPrefs.GetInt("score") == 1? "" : "s");
    }

    public void restartGame() {
        PlayerPrefs.SetInt("lives", 10);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

        PlayerPrefs.SetInt("biology", 0);
        PlayerPrefs.SetInt("chemistry", 0);
        PlayerPrefs.SetInt("engineering", 0);
        PlayerPrefs.SetInt("math", 0);
        PlayerPrefs.SetInt("physics", 0);
        PlayerPrefs.SetInt("tech", 0);

        fader.goToScene("Level1");
    }

    public void mainMenu() {
        fader.goToScene("Welcome");
    }
}