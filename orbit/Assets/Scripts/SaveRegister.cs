/*
Register the user's info. Manages motion and info saving.
Valeria Pineda
12/04/2021
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    int currScreen;
    // Distance in x between registers
    int moveIt;

    // Start is called before the first frame update
    void Start()
    {
        currScreen = 0;
        moveIt = 700;

        PlayerPrefs.SetInt("lives", 10);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

        PlayerPrefs.SetInt("biology", 0);
        PlayerPrefs.SetInt("chemistry", 0);
        PlayerPrefs.SetInt("engineering", 0);
        PlayerPrefs.SetInt("math", 0);
        PlayerPrefs.SetInt("physics", 0);
        PlayerPrefs.SetInt("tech", 0);

        alias.text = "";
        age.text = "";
        
        // ButtonNext.gameObject.SetActive(false);
    }

    public void next() {
        switch (currScreen)
        {
            case 0:
                if (alias.text == "") {}
                else {
                    PlayerPrefs.SetString("alias", alias.text);
                    move(-moveIt);
                    currScreen++;
                }
                break;
            case 1:
                if (age.text == "") {}
                else {
                    PlayerPrefs.SetString("age", age.text);
                    move(-moveIt);
                    currScreen++;
                }
                break;
            case 2:
                if (state.gameObject.activeSelf) {
                    PlayerPrefs.SetString("state", state.text);
                    move(-moveIt);
                    currScreen++;
                }
                break;
            case 3:
                if (gender.gameObject.activeSelf) {
                    PlayerPrefs.SetString("gender", gender.text);
                    move(-moveIt);
                    currScreen++;
                }
                break;
            // Last register, go to Level 1
            case 4:
                if (schoolLevel.gameObject.activeSelf) {
                    PlayerPrefs.SetString("schoolLevel", schoolLevel.text);
                    // Mensaje de yendo a nivel 1
                    SceneManager.LoadScene( "Level1" );
                }
                break;
        }
    }

    public void previous() {
        // Go to home screen
        if(currScreen == 0) {
            SceneManager.LoadScene("Welcome");
        }
        else {
            // Go to screen to the left
            move(moveIt);
            currScreen--;
            ButtonNext.gameObject.SetActive(true);
        }
    }

    // Pretty motion :D (WIP)
    void move(int motion) {
        // If they're put in a parent object, they won't respond well to screen resize
        RectTransform aliasPos = aliasParent.GetComponent<RectTransform>();
        aliasPos.anchoredPosition = new Vector2(aliasPos.anchoredPosition.x + motion, aliasPos.anchoredPosition.y);

        RectTransform agePos = ageParent.GetComponent<RectTransform>();
        agePos.anchoredPosition = new Vector2(agePos.anchoredPosition.x + motion, agePos.anchoredPosition.y);

        RectTransform statePos = stateParent.GetComponent<RectTransform>();
        statePos.anchoredPosition = new Vector2(statePos.anchoredPosition.x + motion, statePos.anchoredPosition.y);

        RectTransform genderPos = genderParent.GetComponent<RectTransform>();
        genderPos.anchoredPosition = new Vector2(genderPos.anchoredPosition.x + motion, genderPos.anchoredPosition.y);
        
        RectTransform schoolLevelPos = schoolLevelParent.GetComponent<RectTransform>();
        schoolLevelPos.anchoredPosition = new Vector2(schoolLevelPos.anchoredPosition.x + motion, schoolLevelPos.anchoredPosition.y);
    }

}
