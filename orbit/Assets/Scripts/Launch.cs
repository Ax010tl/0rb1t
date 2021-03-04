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
    [SerializeField] public float maxVel; 
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.bodyType = RigidbodyType2D.Kinematic; 
    }

    bool canDrag = true; 
    Vector3 dis; 
    void OnMouseDrag()
    {
        if(!canDrag)
            return; 

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        dis = pos - pivot.position; 
        dis.z = 0; //Es 2D, entonces hay que quitarle el componente z
        if(dis.magnitude > dragRange) //si se arrastra fuera del rango definido
        {
            dis = dis.normalized * dragRange; 
        }
        transform.position = dis + pivot.position;
    }

    void OnMouseUp()
    {
        if(!canDrag)
            return; 
        
        canDrag = false; 
        rb.bodyType = RigidbodyType2D.Dynamic; 
        dis.y *= -1; 
        rb.velocity = dis.normalized * maxVel * dis.magnitude / dragRange; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
