/*
Show information of corresponding fact on scene. Also manages
button behaviour.
Valeria Pineda
17/04/2021*/

using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] Text title;
    [SerializeField] Text body;
    [SerializeField] Text score;
    [SerializeField] Text currScore;
    [SerializeField] Image powerup;
    [SerializeField] Image ods;
    [SerializeField] AudioClip interested;
    [SerializeField] AudioClip omitted;
    [SerializeField] AudioClip gameOver;
    [SerializeField] LevelFader fader;
    [SerializeField] FunFactReader ff;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
        // Populate scene
        title.text = ff.getTitle();
        body.text = ff.getFact();
        score.text = ff.getMessage();
        powerup.sprite = ff.getPowerup();
        ods.sprite = ff.getSDG();

        // Print out score
        currScore.text = PlayerPrefs.GetInt("score") + " puntos";
    }

    public void exitGame() 
    {
        source.PlayOneShot(gameOver);
        fader.goToScene("End");
    }

    public void omit()
    {
        source.PlayOneShot(omitted, 2.5f);
        fader.goToScene("Level1");
    }

    public void interesting()
    {
        source.PlayOneShot(interested, 2.5f);
        string subject = ff.getPowerupName(); 
        PlayerPrefs.SetInt(subject, PlayerPrefs.GetInt(subject)+1);
        fader.goToScene("Level1");
    }
}
