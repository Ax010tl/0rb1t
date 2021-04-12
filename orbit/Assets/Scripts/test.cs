using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("lives", 10);        
        PlayerPrefs.SetInt("level", 1);        
        PlayerPrefs.SetInt("score", 0);

        PlayerPrefs.SetInt("biology", 0);
        PlayerPrefs.SetInt("chemistry", 0);
        PlayerPrefs.SetInt("engineering", 0);
        PlayerPrefs.SetInt("math", 0);
        PlayerPrefs.SetInt("physics", 0);
        PlayerPrefs.SetInt("tech", 0);
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.LoadScene( "Level1" );
    }
}
