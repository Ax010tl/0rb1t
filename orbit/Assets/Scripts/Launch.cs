/**
* Permitir que el cohete pueda ser disparado al arrastrar el mouse
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
    //Objeto pivote a partir del cual se crea una circunferencia para lanzar el cohete
    public Transform pivot; 
    //Rango en el que se puede arrastrar el cohete
    public float dragRange;
    //Máxima velocidad que puede alcanzar el cohete
    [SerializeField] public float maxVel; 
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    bool canDrag = true; 
    //Distancia
    Vector3 dis; 
    void OnMouseDrag()
    {
        //Si ya se lanzó 
        if(!canDrag)
            return; 

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        dis = pos - pivot.position; 
        dis.z = 0; //Es 2D, entonces hay que quitarle el componente z
        
        float angleRadians = (float) Math.Atan2(dis.x, -dis.y);
        float angleDegrees = angleRadians * (180/ (float) Math.PI);

        if(dis.magnitude > dragRange) //si se arrastra fuera del rango definido
        {
            dis = dis.normalized * dragRange; 
        }
        transform.position = dis + pivot.position;
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees-42.381f);

    }

    void OnMouseUp()
    {
        //Si ya se lanzó
        if(!canDrag)
            return; 
        
        //Si todavía no se ha lanzado
        canDrag = false; //Ya no se puede volver a lanzar
        rb.bodyType = RigidbodyType2D.Dynamic; 
        //Queremos que funcione como una resortera, entonces debe salir disparado hacia el componente opuesto al que se arrastra
        dis.y *= -1; 
        dis.x *= -1; 
        //La velocidad cambia dependiendo de qué tanto se arrastró en x,y
        rb.velocity = dis.normalized * maxVel * dis.magnitude / dragRange; 
    }
   
    void Update()
    {
        
    }
}
