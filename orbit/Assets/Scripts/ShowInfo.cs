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
    [SerializeField] Image powerup;
    [SerializeField] Image ods;
    [SerializeField] AudioClip interested;
    [SerializeField] AudioClip omitted;
    [SerializeField] AudioClip gameOver;
    [SerializeField] GameObject levelFade;
    AudioSource source;
    LevelFader fader;
    FunFactReader ff;

    // Start is called before the first frame update
    void Start()
    {
        ff = GameObject.FindWithTag("FunFact").GetComponent<FunFactReader>();
        fader = levelFade.GetComponent<LevelFader>();
        source = GetComponent<AudioSource>();
        
        // Populate scene
        title.text = ff.getTitle();
        body.text = ff.getFact();
        score.text = ff.getMessage();
        powerup.sprite = ff.getPowerup();
        ods.sprite = ff.getSDG();
    }

    public void exitGame() 
    {
        source.PlayOneShot(gameOver);
        fader.goToScene("End");
        // So fun fact isn't repeated
        ff.remove();
    }

    public void omit()
    {
        source.PlayOneShot(omitted, 1.5f);
        fader.goToScene("Level1");

        // So fun fact isn't repeated
        ff.remove();
    }

    public void interesting()
    {
        source.PlayOneShot(interested, 1.5f);
        string subject = ff.getPowerupName(); 
        PlayerPrefs.SetInt(subject, PlayerPrefs.GetInt(subject)+1);
        fader.goToScene("Level1");

        // So fun fact isn't repeated
        ff.remove();
    }
}
