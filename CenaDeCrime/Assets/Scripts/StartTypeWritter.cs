using UnityEngine;
using System.Collections;
using TMPro;
public class StartTypeWritter : MonoBehaviour
{
    [SerializeField] float typeWritterSpeed = 0.05f;
    TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(Typewritter(text.text));
    }
    IEnumerator Typewritter(string textToWrite)
    {
        text = GetComponent<TMP_Text>();
        text.text = "";
        for (int i = 0; i < textToWrite.Length; i++)
        {
            text.text += textToWrite[i];
            yield return new WaitForSeconds(typeWritterSpeed);
        }
    }
}
