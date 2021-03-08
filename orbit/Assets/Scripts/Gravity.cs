using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour 
{
    [SerializeField] GameObject planet;
    [SerializeField] double G; // Start with 6.67*Math.Pow(10,-11)
    [SerializeField] double minDistance;
    double speed;
    Vector3 speedDirection;
    Rigidbody2D bodyRocket;
    Rigidbody2D bodyPlanet;
    double massRocket;
    double massPlanet;
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
        transform.rotation = Quaternion.Euler(0, 0, -angleDegrees);
    }
   
    void Start() {
        bodyRocket = GetComponent<Rigidbody2D>(); 
        bodyPlanet = planet.GetComponent<Rigidbody2D>();

        minDistance = planet.GetComponent<Renderer>().bounds.size.x/2;

        massRocket = bodyRocket.mass;        
        massPlanet = bodyPlanet.mass;
    }

    void Update()
    {
        speedDirection = bodyRocket.velocity;
        speed = bodyRocket.velocity.magnitude;
        distanceDirection = getDistanceDirection();
        distanceScalar = getDistanceMagnitude();
        
        Vector3 force = -getGravitationalForce();
        
        /* if(
            Math.Abs(transform.position.x - bodyPlanet.transform.position.x) < 0.01f &&
            Math.Abs(transform.position.y - bodyPlanet.transform.position.y) < 0.01f
        ) return; */

        bodyRocket.AddForce(force);
        updateTrayectoryAngle();
    }
}