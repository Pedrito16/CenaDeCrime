using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
public class DialogueController : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI npcNameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("TypeWritter")]
    [SerializeField] bool useTypeWritter = true;
    [SerializeField] float typeWritterSpeed = 0.05f;
    void Awake()
    {
        GameManager.instance.dialogueController = this;
    }
    public void WriteDialogue(string text)
    {
        if (useTypeWritter)
            StartCoroutine(TypeWritter(text));
        else
            dialogueText.text = text;
    }
    public void SetActive(bool active)
    {
        npcNameText.gameObject.SetActive(active);
        dialogueText.gameObject.SetActive(active);
    }
    IEnumerator TypeWritter(string text)
    {
        char[] characters = text.ToCharArray();
        for(int i = 0; i < characters.Length; i++)
        {
            dialogueText.text += characters[i];
            yield return new WaitForSeconds(typeWritterSpeed);
        }
    }
}
