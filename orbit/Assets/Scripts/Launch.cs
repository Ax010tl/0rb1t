/**
* Allows to drag the rocket in order to launch. (more drag = more velocity)
Lulú Badillo
03/03/2021
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Launch : MonoBehaviour
{
    //Pivot object 
    public Transform pivot; 
    //Drag range
    public float dragRange;
    //Max velocity 
    [SerializeField] public float maxVel; 
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    public bool canDrag = true; 
    //Distance
    public Vector3 dis; 
    void OnMouseDrag()
    {
        //If it has been launched
        if(!canDrag)
            return; 

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        dis = pos - pivot.position; 
        dis.z = 0; //It's 2D, so component z doesn't matter
        
        float angleRadians = (float) Math.Atan2(dis.x, -dis.y);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);

        if(dis.magnitude > dragRange) //if the rocket is dragged outside of the allowed range
        {
            dis = dis.normalized * dragRange; 
        }
        transform.position = dis + pivot.position;
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees-42.381f);

    }

    void OnMouseUp()
    {
        //If the rocket has been launched
        if(!canDrag)
            return; 
        
        //If it hasnt been launched
        canDrag = false; //Do not allow to launch again
        rb.bodyType = RigidbodyType2D.Dynamic; 
        //It works like a slingshot, so the rocket must move in the opposite direction than the user dragged it
        dis.y *= -1; 
        dis.x *= -1; 
        //Velocity changes acording to how far the rocket was dragged
        rb.velocity = dis.normalized * maxVel * dis.magnitude / dragRange; 
    }
   
    void Update()
    {
        
    }
}
