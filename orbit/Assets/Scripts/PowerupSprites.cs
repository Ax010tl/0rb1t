/*
Show all 6 STEM discipline powerups before showing
random ones.
Valeria Pineda
19/04/2021
*/

using UnityEngine;

public class PowerupSprites : MonoBehaviour
{
    Sprite[] sprites;
    FunFactReader facts;
    // Start is called before the first frame update
    void Start()
    {
        facts = GameObject.FindWithTag("FunFact").GetComponent<FunFactReader>();
        sprites = GetComponent<RandomSprites>().list;

        // So all sciences are displayed at least once until level 7
        if(facts.factsViewed < 6) {
            GetComponent<SpriteRenderer>().sprite = sprites[facts.factsViewed];
            facts.factsViewed++;
        }
        else {
            GetComponent<RandomSprites>().enabled = true;
        }
    }
}
