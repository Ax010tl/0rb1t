/*
Read and save all STEM facts from text file. Also contains functions 
other scripts need to manipulate said data.
Valeria Pineda
16/04/2021
*/

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class FunFactReader : MonoBehaviour
{
    public int scoreDelta {
        get { return PlayerPrefs.GetInt("scoreDelta"); }
        set { PlayerPrefs.SetInt("scoreDelta", value); }
    }
    [SerializeField] Sprite[] sdg;
    [SerializeField] Sprite[] powerups;
    string[] lines;
    List<string[]> facts;
    public int factWanted;

    // So list has all data loaded whenever called
    private void Awake() {
        read();
    }

    public void read()
    {
        factWanted = PlayerPrefs.GetInt("level") - 2;
        string[] split;
        facts = new List<string[]>();

        // Save data from text file
        string texty = Resources.Load<TextAsset>("funfacta").text;
        lines = texty.Split('\n');
        // lines = System.IO.File.ReadAllLines("Assets/Text/funfacta.txt");
        foreach (string line in lines)
        {
            // Divide by subject, sprite name, SDO, message and fun fact
            split = line.Split(';');
            facts.Add(split);
        }

        // Remove unused memory spaces :3
        facts.TrimExcess();
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
    public Sprite getLevelPowerup() {
        // Find sprite in the array that has the same name as the one on the text file
        int index = factWanted+1;
        Sprite res = Array.Find(powerups, s => s.name.Equals(facts[index][1]));
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

    public void randomize() {
        System.Random rng = new System.Random();
		
		// Fisher-Yates shuffle from https://stackoverflow.com/questions/273313/randomize-a-listt
		int n = lines.Length;  
		while (n > 1) {
			n--;  
			int k = rng.Next(n + 1);  
			string value = lines[k];
			lines[k] = lines[n];  
			lines[n] = value;  
		}

        // Write randomized facts into text file
		using (StreamWriter outputFile = new StreamWriter("Assets/Resources/funfacta.txt"))
        {
            foreach (string line in lines) {
                // Make sure blank lines are ignored
                if(line != "") {
                    outputFile.WriteLine(line);
                }
            }
        }
	}
}
