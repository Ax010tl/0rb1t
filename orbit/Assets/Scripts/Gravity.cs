/*
Control gravitational pull on rocket. Also changes rocket orientation
and camera size.
Lalo Villalpando
05/03/2021
*/

using System;
using UnityEngine;

public class Gravity : MonoBehaviour 
{
    // Public properties
    [SerializeField] GameObject planet;
    [SerializeField] GameObject background;
    public Camera mainCamera;
    [SerializeField] double G; // Start with 6.67*Math.Pow(10,-11)
    // Rigid bodies
    Rigidbody2D bodyRocket;
    Rigidbody2D bodyPlanet;
    // Rigid bodies masses
    double massRocket;
    double massPlanet;
    // Speed
    double speed;
    Vector3 speedDirection;
    // Distance
    double minDistance;
    double distanceScalar;
    Vector3 distanceDirection;

    Vector3 getDistanceDirection(){
        Vector3 dist = transform.position - planet.transform.position;
        return dist.normalized;
    }

    double getDistanceMagnitude(){
        Vector3 dist = transform.position - planet.transform.position;
        return dist.magnitude + minDistance;
    }

    Vector3 getGravitationalForce(){
        double magnitude = (G*massPlanet*massRocket)/Math.Pow(distanceScalar, 2);
        return distanceDirection*((float) magnitude);
    }

    void updateTrayectoryAngle(){
        if(bodyRocket.velocity.x == 0 && bodyPlanet.velocity.y == 0) return;
        float angleRadians = (float) Math.Atan2(bodyRocket.velocity.x, bodyRocket.velocity.y);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);
        transform.rotation = Quaternion.Euler(0, 0, -angleDegrees-42.381f);
    }
   
    void Start() {
        bodyRocket = GetComponent<Rigidbody2D>(); 
        bodyPlanet = planet.GetComponent<Rigidbody2D>();

        minDistance = planet.GetComponent<Renderer>().bounds.size.x/2;

        massRocket = bodyRocket.mass;        
        massPlanet = bodyPlanet.mass;

        G = PlayerPrefs.GetInt("level") + 7;
        if(G>30){
            G = 30; 
        }
    }

    void Update()
    {
        speedDirection = bodyRocket.velocity;
        speed = bodyRocket.velocity.magnitude;
        distanceDirection = getDistanceDirection();
        distanceScalar = getDistanceMagnitude();

        Vector3 force = -getGravitationalForce();

        mainCamera.orthographicSize = Mathf.Clamp(1.0f * (float) distanceScalar, 3, 11.02718f);

        bodyRocket.AddForce(force);
        updateTrayectoryAngle();
    }
}