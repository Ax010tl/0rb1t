/*
This class is the manager of the game's system. 
It allows us to keep track of the player's lives, score and level. 
Valeria Pineda
10/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] Text livesBox;
    [SerializeField] Text scoreBox;
    [SerializeField] Text levelBox;
    [SerializeField] string livesText;
    [SerializeField] string scoreText;
    [SerializeField] string levelText;
    [SerializeField] Text livesAlert;
    [SerializeField] Text scoreAlert;

    int score, lives, level;
    
    void Update() {
        score = PlayerPrefs.GetInt("score");
        lives = PlayerPrefs.GetInt("lives");
        level = PlayerPrefs.GetInt("level");
    }

    public void changeScore(int num) {
        score += num;
        PlayerPrefs.SetInt("score", score);
        if (score%10 == 0) {
            changeLives(1);
        }
        
        if(num < 0) {
            StartCoroutine(fade(scoreAlert, num.ToString()));
        }
        else {
            StartCoroutine(fade(scoreAlert, "+" + num));
        }
    }

    public void changeLives(int num) {
        if (lives+num <= 10) {
            lives += num;
            PlayerPrefs.SetInt("lives", lives);
        }

        if(num < 0) {
            StartCoroutine(fade(livesAlert, num.ToString()));
        }
        else {
            StartCoroutine(fade(livesAlert, "+" + num));
        }
    }

    public void changeLevel(int num) {
        level += num;
        PlayerPrefs.SetInt("level", level);
    }

    // Use this method when displaying inside level
    public void displayAll() {
        levelBox.text = levelText + level.ToString();
        
        // Shows lives as cool progress bar
        string livesEmoji = "";
        for(int i=0; i<lives; i++){
            livesEmoji += "■";
        }
        for(int i=0; i<10-lives; i++){
            livesEmoji += "□";
        }

        // Shows score as symbols :3
        string scoreEmoji = "";
        for(int i = 0; i < score; i++){
            scoreEmoji += "|";
        }
        livesBox.text = livesText + livesEmoji;
        scoreBox.text = scoreText + scoreEmoji;
    }

    // Use this method when displaying outside level
    public void showAll() {
        levelBox.text = levelText + level.ToString();
        livesBox.text = livesText + lives.ToString();
        scoreBox.text = scoreText + score.ToString();
    }

    IEnumerator fade(Text textBox, string text)
    {
        textBox.text = text;
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1);
        RectTransform pos = textBox.GetComponent<RectTransform>();
        while (textBox.color.a > 0.0f)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a - Time.deltaTime);
            pos.anchoredPosition = new Vector3(pos.anchoredPosition.x, pos.anchoredPosition.y + 0.3f);
            yield return null;
        }
    }
}