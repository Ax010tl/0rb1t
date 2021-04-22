/*
Move objects so it looks like they're levitating.
Valeria Pineda
07/04/2021
*/

using UnityEngine;

public class FloatyBoi : MonoBehaviour
{
    float speed, min, max;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = -0.002f;
        min = transform.position.y - 0.2f;
        max = transform.position.y + 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + speed,
            0
        );

        if (transform.position.y <= min || transform.position.y >= max)
            speed *= -1;
    }
}
