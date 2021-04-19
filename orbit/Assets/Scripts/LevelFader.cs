/*
Fade from current scene to next.
Must be set in the same object as animator.
Valeria Pineda
14/04/2021
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    public Animator animator;
    string levelToLoad;

    public void goToScene(string level) {
        fadeToLevel();
        levelToLoad = level;
    }

    // Setting starts fade out animation
    void fadeToLevel() {
        animator.SetTrigger("fadeOut");
    }

    // When fade out animation is done it calls this function
    public void onFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }
}
