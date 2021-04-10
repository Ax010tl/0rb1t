/*
10/04/2021
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text livesText;
    public Text scoreText;
    public Text levelText;
    public static int lives; 
    public static int score; 
    public static int level; 
    // Start is called before the first frame update
    void Start()
    {
        // Change text to show level 
        levelText.text = "Nivel: " + PlayerPrefs.GetInt("level"); 
        // Change text to show score
        scoreText.text = "Puntaje: " + PlayerPrefs.GetInt("score"); 
        //C hange text to show lives
        livesText.text = "Vidas: " + PlayerPrefs.GetInt("lives"); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int getLives(){
        return lives; 
    }

    int getScore(){
        return score; 
    }

    int getLevel(){
        return level; 
    }

    void setLives(int num){
        lives = num; 
    }

    void setScore(int num){
        score = num;
    }

    void setLevel(int num){
        level = num;
    }
}
