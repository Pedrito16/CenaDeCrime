using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    public string npcName;
    [TextArea]
    public string[] lines;
    public UnityEvent onLinesEnd;
}
public class NPC : Interactable
{
    [Header("Dialogue")]
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] bool loopDialogueOnEnd;
    

    [Header("Debug")]
    [SerializeField] int dialogueIndex;
    [SerializeField] int currentLineIndex;
    [SerializeField] bool isOnDialogue;

    DialogueController dialogueController;
    protected override void Start()
    {
        base.Start();
        dialogueController = gameManager.dialogueController;
        dialogueIndex = 0;
    }
    public override void Interact()
    {
        bool hasDialogue = dialogueIndex < dialogues.Length;
        gameManager.interactUI.SetTextAtive(false);

        if (hasDialogue)
           WriteLine();
    }
    void WriteLine()
    {
        Dialogue currentDialogue = dialogues[dialogueIndex];
        string[] currentLines = currentDialogue.lines;
        if (currentLineIndex < currentLines.Length)
        {
            dialogueController.WriteDialogue(currentLines[currentLineIndex], currentDialogue.npcName, () => { currentLineIndex++; });
            if(!dialogueController.isOnDialogue)
                currentLineIndex++;
        }
        else
        {
            EndDialogue();
            dialogues[dialogueIndex - 1].onLinesEnd?.Invoke();
            print("onLinesEnd");
        }
    }
    void EndDialogue()
    {
        if(dialogueIndex < dialogues.Length)
        {
            dialogueIndex++;
            currentLineIndex = 0;
        }

        if (loopDialogueOnEnd && dialogueIndex >= dialogues.Length) //para loopar dialogo
            dialogueIndex = 0;

        if(dialogueIndex >= dialogues.Length)
        CloseDialogue();
    }
    void CloseDialogue()
    {
        dialogueController.SetActive(false);
        currentLineIndex = 0;
        isOnDialogue = false;
    }
    #region OnRaycast
    public override void OnRaycastEnter()
    {
        if(!enabled) return;

        base.OnRaycastEnter();
    }
    public override void OnRaycastExit()
    {
        if (!enabled) return;

        base.OnRaycastExit();
        CloseDialogue();
    }
    #endregion
}
