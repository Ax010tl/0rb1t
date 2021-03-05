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

    double getDistanceScalar(){
        return Math.Sqrt(
            Math.Pow( (double) transform.position.x - (double) planet.transform.position.x , 2) +
            Math.Pow( (double) transform.position.y - (double) planet.transform.position.y , 2)
        ) + minDistance;
    }
    Vector3 getDistanceDirection(){
        Vector3 dist = transform.position - planet.transform.position;
        return dist.normalized;
    }

    Vector3 getGravitationalForce(){
        double magnitude = (G*massPlanet*massRocket)/Math.Pow(distanceScalar, 2);
        return distanceDirection*((float) magnitude);
    }

   
    void Start() {
        bodyRocket = GetComponent<Rigidbody2D>(); 
        bodyPlanet = planet.GetComponent<Rigidbody2D>();
        massRocket = bodyRocket.mass;        
        massPlanet = bodyPlanet.mass;
    }

    void Update()
    {
        speedDirection = bodyRocket.velocity;
        speed = bodyRocket.velocity.magnitude;
        distanceScalar = getDistanceScalar();
        distanceDirection = getDistanceDirection();
        Vector3 force = -getGravitationalForce();
        bodyRocket.AddForce(force);
    }
}