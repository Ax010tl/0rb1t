/*
Read and save all STEM facts from text file. Also contains functions 
other scripts need to manipulate said data.
Valeria Pineda
16/04/2021
*/

using System;
using System.Collections.Generic;
using UnityEngine;

public class FunFactReader : MonoBehaviour
{
    public int scoreDelta;
    public bool tutorial;
    public int factsViewed;
    [SerializeField] Sprite[] sdg;
    [SerializeField] Sprite[] powerups;
    List<string[]> facts;
    int factWanted;

    // Start is called before the first frame update
    void Start()
    {
        string[] lines, split;
        facts = new List<string[]>();

        // Save data from text file to list
        lines = System.IO.File.ReadAllLines("Assets/Text/funfacta.txt");
        foreach (string line in lines)
        {
            // Divide by subject, sprite name, SDO, message and fun fact
            split = line.Split(';');
            facts.Add(split);
        }

        // Remove unused memory spaces :3
        facts.TrimExcess();

        // So the game remembers which facts have been displayed
        DontDestroyOnLoad(this.gameObject);

        // So tutorial is shown the first time player is on Level 1
        tutorial = true;
        factsViewed = 0;
    }

    // Check if there are any fun facts left for that subject
    public bool factsExist(string subject) {
        tutorial = false;
        // Check if any facts remain
        if(facts.Count > 0) {
            for(int i = 0; i < facts.Count; i++) {
                // Check subject
                if(facts[i][1] == subject) {
                    factWanted = i;
                    return true;
                }
            }
        }
        return false;
    }

    // Getters
    public string getTitle() {
        return facts[factWanted][0];
    }
    public Sprite getPowerup() {
        // Find sprite in the array that has the same name as the one on the text file
        Sprite res = Array.Find(powerups, s => s.name.Equals(facts[factWanted][1]));
        return res;
    }
    public string getPowerupName() {
        return facts[factWanted][1];
    }
    public Sprite getSDG() {
        // Find sprite in the array that has the same name as the one on the text file
        Sprite res = Array.Find(sdg, s => s.name.Equals(facts[factWanted][2]));
        return res;
    }
    public string getMessage() {
        if(scoreDelta == 0) {
            return facts[factWanted][3];
        }
        else {
            return "+" + scoreDelta.ToString() + " punto" + (scoreDelta == 1 ? "" : "s");
        }
    }
    public string getFact() {
        return facts[factWanted][4];
    }

    public void remove() {
        facts.RemoveAt(factWanted);
    }
}
