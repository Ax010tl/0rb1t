/*
Control gravitational pull on rocket. Also changes rocket orientation
and camera size.
Lalo Villalpando
05/03/2021
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gravity : MonoBehaviour 
{
    public double G; // Start with 6.67*Math.Pow(10,-11)
    [SerializeField] GameObject rocket;
    [SerializeField] Text gravityText;
    [SerializeField] string bodyText;
    // threshold is when it starts being randomized
    // begin gives the starting values
    [SerializeField] int min, max, threshold, begin;
    // Rigid bodies
    Rigidbody2D bodyRocket;
    Rigidbody2D bodyPlanet;
    // Rigid bodies masses
    double massRocket;
    double massPlanet;
    // Distance from center of planet to its edges
    double minDistance;

    Vector3 getDistanceDirection(){
        Vector3 dist = rocket.transform.position - transform.position;
        return dist.normalized;
    }

    public double getDistanceMagnitude(){
        Vector3 dist = rocket.transform.position - transform.position;
        return dist.magnitude + minDistance;
    }

    public Vector3 getGravitationalForce(){
        double magnitude = (G*massPlanet*massRocket)/Math.Pow(getDistanceMagnitude(), 2);
        return -getDistanceDirection()*((float) magnitude);
    }

    void Start() {
        bodyRocket = rocket.GetComponent<Rigidbody2D>(); 
        bodyPlanet = GetComponent<Rigidbody2D>();

        minDistance = GetComponent<Renderer>().bounds.size.x/2;

        massRocket = bodyRocket.mass;        
        massPlanet = bodyPlanet.mass;

        // How we manage Gravity for each level
        G = begin+2 - 2*PlayerPrefs.GetInt("level");
        if(G < threshold) {
            G = UnityEngine.Random.Range(min, max);
        }

        // Text with the planet's gravity, which disappears after 2 seconds
        gravityText.text = "Gravedad de" + bodyText + G.ToString(); 
        StartCoroutine(disappearGravity());
    }

    IEnumerator disappearGravity ()
    {
        // Disappear gravity text after 15 seconds
        yield return new WaitForSeconds(15); 
        gravityText.enabled = false;
    }
}