using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // [SerializeField] GameObject planet;
    // [SerializeField] GameObject moon;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject background;
    [SerializeField] Gravity planet;
    Rigidbody2D bodyRocket;
    // Start is called before the first frame update
    void Start()
    {
        bodyRocket = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        mainCamera.orthographicSize = Mathf.Clamp(1.0f * (float) planet.getDistanceMagnitude(), 3, 11.02718f);

        // Give the illusion the stars are far away (parallax)
        float dis_boi = 0.21f * mainCamera.orthographicSize;
        background.transform.localScale = new Vector3(dis_boi, dis_boi, 0);

        Vector3 sumForces = new Vector3(0,0,0);

        GameObject[] planetObjs = GameObject.FindGameObjectsWithTag("Planet");
        foreach(GameObject obj in planetObjs) {
            sumForces += obj.GetComponent<Gravity>().getGravitationalForce();
        }
        bodyRocket.AddForce(sumForces);
        updateAngle();
    }

    void updateAngle(){
        if(bodyRocket.velocity.x == 0) return;
        float angleRadians = (float) Math.Atan2(bodyRocket.velocity.x, bodyRocket.velocity.y);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);
        transform.rotation = Quaternion.Euler(0, 0, -angleDegrees-42.381f);
    }
}
