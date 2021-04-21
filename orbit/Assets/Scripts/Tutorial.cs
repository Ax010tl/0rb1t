/*
Control Behaviour of tutorial and ensure it shows things 
with proper timing and only the first time player goes
through Level 1.
Valeria Pineda
19/04/2021
*/

using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject drag;
    [SerializeField] GameObject orbit;
    [SerializeField] GameObject powerup;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject all;
    bool canDrag;
    int orbits;
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Only shows tutorial first time player is in level 1
        if(PlayerPrefs.GetInt("tutorial") == 1) {
            drag.SetActive(true);
            orbit.SetActive(false);
            powerup.SetActive(false);
            orbits = rocket.GetComponent<Goal>().numberOfRevolutions;
        }
        else {
            // Deactivate parent object so they are not shown in spite of the code below
            all.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Once rocket is released
        if (!rocket.GetComponent<Launch>().canDrag && count == 1) {
            drag.SetActive(false);
            orbit.SetActive(true);
            StartCoroutine(nextSigns());
            count++;
        }
    }

    IEnumerator nextSigns() {
        yield return new WaitForSeconds(5.0f);
        orbit.SetActive(false);
        powerup.SetActive(true);

        // So tutorial isn't repeated
        PlayerPrefs.SetInt("tutorial", 0);
        yield return new WaitForSeconds(5.0f);
        powerup.SetActive(false);
    }
}
