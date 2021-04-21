/*
Register the user's info. Manages motion and info saving.
Valeria Pineda
12/04/2021
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveRegister : MonoBehaviour
{
    [SerializeField] Text alias;
    [SerializeField] Text age;
    [SerializeField] Text state;
    [SerializeField] Text gender;
    [SerializeField] Text schoolLevel;
    [SerializeField] Text aliasParent;
    [SerializeField] Text ageParent;
    [SerializeField] Text stateParent;
    [SerializeField] Text genderParent;
    [SerializeField] Text schoolLevelParent;
    [SerializeField] Button ButtonNext;
    [SerializeField] LevelFader fader;
    [SerializeField] FunFactReader ff;
    int currScreen;
    int yPos;

    // Start is called before the first frame update
    void Start()
    {
        currScreen = 0;

        // Default settings for beginning of game
        PlayerPrefs.SetInt("lives", 10);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

        // Create PlayerPrefs for STEM subjects interest
        PlayerPrefs.SetInt("biology", 0);
        PlayerPrefs.SetInt("chemistry", 0);
        PlayerPrefs.SetInt("engineering", 0);
        PlayerPrefs.SetInt("math", 0);
        PlayerPrefs.SetInt("physics", 0);
        PlayerPrefs.SetInt("tech", 0);

        // So tutorial is only shown on the player's first time in level 1
        PlayerPrefs.SetInt("tutorial", 1);

        yPos = 123;

        ff.randomize();
    }

    public void next() {
        // Ensures user has entered data before going to next register
        switch (currScreen)
        {
            case 0:
                if (alias.text == "") {}
                else {
                    PlayerPrefs.SetString("alias", alias.text);
                    StartCoroutine(coolMotion(-1));
                    currScreen++;
                }
                break;
            case 1:
                if (age.text == "") {}
                else {
                    PlayerPrefs.SetString("age", age.text);
                    StartCoroutine(coolMotion(-1));
                    currScreen++;
                }
                break;
            case 2:
                if (state.text != "Escoge tu estado...") {
                    PlayerPrefs.SetString("state", state.text);
                    StartCoroutine(coolMotion(-1));
                    currScreen++;
                }
                break;
            case 3:
                if (gender.text != "Escoge tu género...") {
                    PlayerPrefs.SetString("gender", gender.text);
                    StartCoroutine(coolMotion(-1));
                    currScreen++;
                }
                break;
            // Last register, go to Level 1
            case 4:
                if (schoolLevel.text != "Escoge uno...") {
                    PlayerPrefs.SetString("schoolLevel", schoolLevel.text);
                    fader.goToScene("Level1");
                }
                break;
        }
    }

    public void previous() {
        // Go to home screen
        if(currScreen == 0) {
            fader.goToScene("Welcome");
        }
        else {
            // Go to screen to the left
            StartCoroutine(coolMotion(1));
            currScreen--;
        }
    }

    // Pretty motion :D
    IEnumerator coolMotion(int direction) {
        // If they're put in a parent object, they won't respond well to screen resize
        RectTransform aliasPos = aliasParent.GetComponent<RectTransform>();
        RectTransform agePos = ageParent.GetComponent<RectTransform>();
        RectTransform statePos = stateParent.GetComponent<RectTransform>();
        RectTransform genderPos = genderParent.GetComponent<RectTransform>();
        RectTransform schoolLevelPos = schoolLevelParent.GetComponent<RectTransform>();

        // Move to the next register (700 away)
        for (int i = 0; i < 35; i++)
        {
            aliasPos.anchoredPosition = new Vector2(aliasPos.anchoredPosition.x +20*direction, yPos);
            agePos.anchoredPosition = new Vector2(agePos.anchoredPosition.x +20*direction, yPos);
            statePos.anchoredPosition = new Vector2(statePos.anchoredPosition.x +20*direction, yPos);
            genderPos.anchoredPosition = new Vector2(genderPos.anchoredPosition.x +20*direction, yPos);
            schoolLevelPos.anchoredPosition = new Vector2(schoolLevelPos.anchoredPosition.x +20*direction, yPos);

            yield return null;
        }
    }

}
