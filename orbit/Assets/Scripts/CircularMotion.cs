/*
Script for moving objects around another one constantly (circular motion).
No need to worry about position :)
Valeria Pineda
09/03/2021
*/

using System;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    [SerializeField] GameObject planet;
    public float radius;
    public float speed;
    public bool loopy;      // when enabled, orbit is elliptical
    public int direction;   // greater/equal than zero goes right, negative goes left
    float t;
    float offset;           // for elliptical orbit

    private void Start()
    {
        // place object above planet, depending on radius
        transform.position = new Vector3(planet.transform.position.x, planet.transform.position.y + radius, 0);

        // random elliptical orbit
        UnityEngine.Random.InitState((int)Time.unscaledTime);
        if (loopy)
        {
            offset = UnityEngine.Random.Range(30, 60);
        }

        if (direction >= 0)
        {
            speed *= -1;
        }
    }

    void Update()
    {
        // trig :D
        t += Time.deltaTime * speed;

        float x = (float)Math.Cos(t + offset) * radius;
        float y = (float)Math.Sin(t - offset) * radius;
        float z = 0;

        transform.position = new Vector3(x, y, z);
    }
}