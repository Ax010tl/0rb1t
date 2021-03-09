// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Planet : MonoBehaviour
{
    [SerializeField] Sprite[] list;

    // Start is called before the first frame update
    void Start()
    {
        Random.seed++;
        GetComponent<SpriteRenderer>().sprite = list[ Random.Range(0,3) ];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
