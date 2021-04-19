/*
When called, functions will play sound effect corresponding
to game event.
Valeria Pineda
16/04/2021
*/

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Sounds required for level
    [SerializeField] AudioClip scoreAdd;
    [SerializeField] AudioClip scoreTake;
    [SerializeField] AudioClip levelAdd;
    [SerializeField] AudioClip livesAdd;
    [SerializeField] AudioClip livesTake;
    [SerializeField] AudioClip gameOver;
    // this one will play all sounds
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Functions for audios to play
    public void scorePlusSound() {
        source.PlayOneShot(scoreAdd);
    }
    public void scoreMinusSound() {
        source.PlayOneShot(scoreTake);
    }
    public void levelChangeSound() {
        source.PlayOneShot(levelAdd, 1.5f);
    }
    public void livesPlusSound() {
        source.PlayOneShot(livesAdd);
    }
    public void livesMinusSound() {
        source.PlayOneShot(livesTake);
    }
    public void gameOverSound() {
        source.PlayOneShot(gameOver, 1.5f);
    }
}
