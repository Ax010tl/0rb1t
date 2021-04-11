using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeyBoi : MonoBehaviour
{
    Text textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();
    }

    // can ignore the update, it's just to make the coroutines get called for example
    void Update()
    {
        int yas = +1;
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(fade(textBox, yas.ToString()));
            // StartCoroutine(uppy());
        }
    }
 
    IEnumerator fade(Text textBox, string text)
    {
        textBox.text = text;
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 1);
        RectTransform pos = textBox.GetComponent<RectTransform>();
        while (textBox.color.a > 0.0f)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a - Time.deltaTime);
            pos.anchoredPosition = new Vector3(pos.anchoredPosition.x, pos.anchoredPosition.y + 0.3f);
            yield return null;
        }
    }
}