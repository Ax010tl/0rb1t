using System;
using UnityEngine;

public class WelcomeScreen : MonoBehaviour
{
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject levelFade;
    LevelFader fader;
    Vector3 dis;
    float t = 0;

    private void Start() {
        fader = levelFade.GetComponent<LevelFader>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        updateTrayectoryAngle();
    }

    void move() {
        t += Time.deltaTime * 0.2f;

        float x = (float)Math.Cos(t) * 2;
        float y = (float)Math.Sin(t) * 2;
        float z = 0;

        rocket.transform.position = new Vector3(x, y, z);
    }
    void updateTrayectoryAngle(){
        dis = rocket.transform.position - planet.transform.position;
        float angleRadians = (float) Math.Atan2(dis.y, dis.x);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);
        rocket.transform.rotation = Quaternion.Euler(0, 0, angleDegrees-42.381f);
    }

    public void startGame() {
        fader.goToScene("Register");
    }
}
