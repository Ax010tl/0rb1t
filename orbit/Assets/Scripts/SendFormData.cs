/*
Send Player Prefs as HTML form. Adapted from example in class
Written by Octavio Navarro and Gilberto Echeverría.
13/04/2021
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendFormData : MonoBehaviour
{
    // The route for the api that inserts data.
    string apiURL = "https://steam-orbit.herokuapp.com/api/gamedata";

    private void Start() 
    {
    }

    // We need to start a coroutine that calls the request
    public IEnumerator uploadData()
    {
        // Unity sends a form, just as a html form. 
        WWWForm form = new WWWForm();

        // We need to create the form first, by manually adding fields. 
        // These fields match the names of the columns in the database.
        form.AddField("alias", PlayerPrefs.GetString("alias"));
        form.AddField("age", PlayerPrefs.GetString("age"));
        form.AddField("state", PlayerPrefs.GetString("state"));
        form.AddField("gender", PlayerPrefs.GetString("gender"));
        form.AddField("schoolLevel", PlayerPrefs.GetString("schoolLevel"));
        form.AddField("biology", PlayerPrefs.GetInt("biology"));
        form.AddField("chemistry", PlayerPrefs.GetInt("chemistry"));
        form.AddField("physics", PlayerPrefs.GetInt("physics"));
        form.AddField("engineering", PlayerPrefs.GetInt("engineering"));
        form.AddField("tech", PlayerPrefs.GetInt("tech"));
        form.AddField("math", PlayerPrefs.GetInt("math"));
        form.AddField("score", PlayerPrefs.GetInt("score"));

        Debug.Log(form);

        // We create a request that makes a post to the url, and sends the form we just created.
        using (UnityWebRequest request = UnityWebRequest.Post(apiURL, form))
        {
            // The yield return line is the point at which execution will pause, and be resumed after the request ends.
            yield return request.SendWebRequest();

            // If there are no errors...
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                // We get the response text and log it in the console.
                Debug.Log(request.downloadHandler.text);
                Debug.Log("Form upload complete!");
            }
        }
    }
}