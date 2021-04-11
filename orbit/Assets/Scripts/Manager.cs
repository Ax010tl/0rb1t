/*
This class is the manager of the game's system. 
It allows us to keep track of the player's lives, score and level. 
Valeria Pineda
10/04/2021
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text livesBox;
    public Text scoreBox;
    public Text levelBox;
    public string livesText;
    public string scoreText;
    public string levelText;

    int score, lives, level;
    // Start is called before the first frame update
    void Start(){}

    void Update() {
        score = PlayerPrefs.GetInt("score");
        lives = PlayerPrefs.GetInt("lives");
        level = PlayerPrefs.GetInt("level");
        displayAll();
    }

    public void changeScore(int num) {
        score += num;
        PlayerPrefs.SetInt("score", score);
        scoreBox.text = scoreText + score.ToString();
    }

    public void changeLives(int num) {
        lives += num;
        PlayerPrefs.SetInt("lives", lives);
        livesBox.text = livesText + lives.ToString();
    }

    public void changeLevel(int num) {
        level += num;
        PlayerPrefs.SetInt("level", level);
        levelBox.text = levelText + level.ToString();
    }

    public void displayAll() {
        // scoreBox.text = scoreText + "⊚⊚⊚⊚";
        // scoreBox.text = scoreText + score.ToString();
        levelBox.text = levelText + level.ToString();
        
        string livesEmoji = "";
        for(int i=0; i<lives; i++){
            livesEmoji += "■";
        }
        for(int i=0; i<10-lives; i++){
            livesEmoji += "□";
        }
        string scoreEmoji = "";
        for(int i = 0; i < score; i++){
            scoreEmoji += "⊚";
        }
        livesBox.text = livesText + livesEmoji; 
        scoreBox.text = scoreText + scoreEmoji;
    }
}