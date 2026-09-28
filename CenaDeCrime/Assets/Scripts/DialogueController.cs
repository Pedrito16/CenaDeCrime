using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueController : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI npcNameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [Header("TypeWritter")]
    [SerializeField] bool useTypeWritter = true;
    [SerializeField] float typeWritterSpeed = 0.05f;

    [Header("References")]
    [SerializeField] GameObject nextSelect;

    [Header("Debug")]
    public bool isOnDialogue;
    string lastText;
    Coroutine typeWritterRoutine;
    void Awake()
    {
        dialogueText.text = "";
        npcNameText.text = "";

        GameManager.instance.dialogueController = this;
        SetActive(false);
        nextSelect.gameObject.SetActive(false);
    }
    public void WriteDialogue(string text, string npcName = "")
    {
        nextSelect.gameObject.SetActive(false);
        dialogueText.text = "";
        npcNameText.text = npcName;

        SetActive(true);

        if (useTypeWritter)
        {
            if (useTypeWritter && isOnDialogue)
            {
                ResetText();
                return;
            }

            isOnDialogue = true;
            lastText = text;

            typeWritterRoutine = StartCoroutine(TypeWritter(text));
        }
        else
            dialogueText.text = text;
    }
    public void ResetText()
    {
        if(typeWritterRoutine != null)
            StopCoroutine(typeWritterRoutine);

        nextSelect.gameObject.SetActive(true);

        isOnDialogue = false;

        dialogueText.text = "";
        dialogueText.text = lastText;

        typeWritterRoutine = null;
    }
    public void SetActive(bool active)
    {
        npcNameText.gameObject.SetActive(active);
        if(!active) nextSelect.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(active);
    }
    IEnumerator TypeWritter(string text)
    {
        char[] characters = text.ToCharArray();
        lastText = text;
        for (int i = 0; i < characters.Length; i++)
        {
            dialogueText.text += characters[i];
            yield return new WaitForSeconds(typeWritterSpeed);
        }
        nextSelect.gameObject.SetActive(true);
        isOnDialogue = false;
        typeWritterRoutine = null;
    }
}
