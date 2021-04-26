/*
This class is the manager of the game's system. 
It allows us to keep track of the player's lives, score and level. 
Valeria Pineda
10/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesBox;
    [SerializeField] Text scoreBox;
    [SerializeField] Text levelBox;
    [SerializeField] string livesText;
    [SerializeField] string scoreText;
    [SerializeField] string levelText;
    [SerializeField] Text livesAlert;
    [SerializeField] Text scoreAlert;
    AudioManager sfx;

    int score, lives, level;
    
    void Start() {
        sfx = GetComponent<AudioManager>();
    }

    void Awake() {
        score = PlayerPrefs.GetInt("score");
        lives = PlayerPrefs.GetInt("lives");
        level = PlayerPrefs.GetInt("level");
    }

    public void changeScore(int num) {
        // So we dont end up with negative scores
        if(score == 0 && num < 0){
            return;
        }
        else{
            score += num;
            PlayerPrefs.SetInt("score", score);
            
            // Subtract score
            if(num < 0) {
                sfx.scoreMinusSound();
                StartCoroutine(fade(scoreAlert, "" + num));
            }
            // Add score
            else {
                if (score%10 == 0 && lives < 10) {
                    changeLives(1);
                }
                sfx.scorePlusSound();
                StartCoroutine(fade(scoreAlert, "+" + num));
            }
        }
        displayAll();
    }

    public void changeLives(int num) {
        lives += num;
        PlayerPrefs.SetInt("lives", lives);

        // Subtract lives
        if(num < 0) {
            if(lives == 0) {
                sfx.gameOverSound();
            } 
            else {
                sfx.livesMinusSound();
            }
            StartCoroutine(fade(livesAlert, num.ToString()));
        }
        // Add lives
        else {
            StartCoroutine(addLives(num));
        }
        displayAll();
    }

    public void changeLevel(int num) {
        level += num;
        PlayerPrefs.SetInt("level", level);
        sfx.levelChangeSound();
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
        string scoreEmoji = "Puntos\n";
        for(int i = 0; i < score; i++){
            scoreEmoji += "|";
        }
        livesBox.text = livesText + livesEmoji;
        scoreBox.text = scoreText + scoreEmoji;
    }

    // Show message when lives and score change
    IEnumerator fade(Text textBox, string text)
    {
        textBox.text = text;
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1);
        RectTransform pos = textBox.GetComponent<RectTransform>();
        while (textBox.color.a > 0.0f)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a - Time.deltaTime);
            pos.anchoredPosition = new Vector3(pos.anchoredPosition.x, pos.anchoredPosition.y + 0.1f);
            yield return null;
        }
    }

    IEnumerator addLives(int num) {
        yield return new WaitForSeconds(0.5f);
        sfx.livesPlusSound();
        StartCoroutine(fade(livesAlert, "+" + num));
    }
}