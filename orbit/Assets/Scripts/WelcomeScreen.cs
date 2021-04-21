/*
Manage behaviours in welcome screen: buttons, movement, audio.
Valeria Pineda
16/04/2021
*/

using System;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject planet;
    [SerializeField] LevelFader fader;
    [SerializeField] GameObject music;
    Vector3 dis;
    float t = 0;

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

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length > 1) {
            Destroy(objs[1]);
        }
        DontDestroyOnLoad(music);
    }
}
