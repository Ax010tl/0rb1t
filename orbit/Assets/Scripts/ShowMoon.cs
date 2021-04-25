using System.Collections;
using UnityEngine;

public class ShowMoon : MonoBehaviour
{
    [SerializeField] GameObject moon;
    [SerializeField] GameObject moonMessage;
    float vel;
    float level;

    private void Awake() {
        level = PlayerPrefs.GetInt("level");

        // After level 12 it shows the moon every two levels
        if(level > 12 && level%2 == 0) {
            moon.SetActive(true);
            vel = level/15;
        }
        // After level 6 it shows the moon every three levels
        else if(level%3 == 0 && level > 5) {
            moon.SetActive(true);
            vel = level/30;
        }
    }

    private void Start() {
        // So the first time it appears is very non-threatening
        if(level == 6) {
            moon.GetComponent<CircularMotion>().speed = 0.05f;
            moonMessage.SetActive(true);
            StartCoroutine(disappear());
        }
        // Moon moves quicker every time it appears
        else if(moon.activeInHierarchy) {
            moon.GetComponent<CircularMotion>().speed = vel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disappear() {
        yield return new WaitForSeconds(4);
        moonMessage.SetActive(false);
    }
}
