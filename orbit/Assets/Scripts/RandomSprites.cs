/*
This class allows us to add multiple sprites, 
so that the colors of the planets and rockets are random. 
*/
// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomSprites : MonoBehaviour
{
    [SerializeField] Sprite[] list;

    // Start is called before the first frame update
    void Start()
    {
        Random.seed++;
        // Random.InitState((int) Time.time);
        GetComponent<SpriteRenderer>().sprite = list[ Random.Range(0,list.Length) ];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
