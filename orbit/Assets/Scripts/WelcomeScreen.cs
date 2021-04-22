/*
Manage behaviours in welcome screen: buttons, movement, audio
and display high score.
Valeria Pineda & Lalo Villalpando
16/04/2021
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WelcomeScreen : MonoBehaviour
{
    string apiURL = "https://steam-orbit.herokuapp.com/api/highscore";
    string highScoreNumber;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject planet;
    [SerializeField] LevelFader fader;
    [SerializeField] GameObject music;
    [SerializeField] Text highScore;
    [SerializeField] Text highScoreShadow;
    Vector3 dis;
    float t = 0;

    void Start()
    {
        // Fetch highest score from API
        StartCoroutine(getHighScore());
    }
    // Update is called once per frame
    void Update()
    {
        move();
        updateTrayectoryAngle();
    }

    // Move rocket around
    void move() {
        t += Time.deltaTime * 0.2f;

        float x = (float)Math.Cos(t) * 2;
        float y = (float)Math.Sin(t) * 2;
        float z = 0;

        rocket.transform.position = new Vector3(x, y, z);
    }

    // Change direction rocket is facing
    void updateTrayectoryAngle(){
        dis = rocket.transform.position - planet.transform.position;
        float angleRadians = (float) Math.Atan2(dis.y, dis.x);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);
        rocket.transform.rotation = Quaternion.Euler(0, 0, angleDegrees-42.381f);
    }

    // Go to register screens
    public void startGame() {
        fader.goToScene("Register");
    }

    // Make sure second instance of music doesn't start if scene is reloaded
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length > 1) {
            Destroy(objs[1]);
        }
        DontDestroyOnLoad(music);
    }

    // Receive high score and show it
    IEnumerator getHighScore()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURL))
        {
            // Request and wait for the desired page
            yield return webRequest.SendWebRequest();
            if(webRequest.isNetworkError || webRequest.downloadHandler.text.Contains("Error")) {
                highScore.enabled = false;
                highScoreShadow.enabled = false;
                highScoreNumber = "0";
            } else {
                highScoreNumber = webRequest.downloadHandler.text;
                highScoreNumber = highScoreNumber.Split(':')[1];
                highScoreNumber = highScoreNumber.Split('}')[0];
                highScore.text = "HIGH SCORE: " +highScoreNumber;
                highScoreShadow.text = "HIGH SCORE: " +highScoreNumber;
            }
        }
    }
}