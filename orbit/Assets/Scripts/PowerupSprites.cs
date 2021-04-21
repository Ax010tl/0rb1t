/*
Show all 6 STEM discipline powerups before showing
random ones.
Valeria Pineda
19/04/2021
*/

using UnityEngine;

public class PowerupSprites : MonoBehaviour
{
    [SerializeField] FunFactReader facts;
    
    // Start is called before the first frame update
    void Start()
    {
        // So all facts are displayed until level 13
        if(PlayerPrefs.GetInt("level") < 13) {
            GetComponent<SpriteRenderer>().sprite = facts.getLevelPowerup();
        }
        else {
            GetComponent<RandomSprites>().enabled = true;
        }
    }
}
