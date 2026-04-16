using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    public string npcName;
    [TextArea]
    public string[] lines;
}
public class NPC : Interactable
{
    [Header("Dialogue")]
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] bool loopDialogueOnEnd;
    

    [Header("Debug")]
    [SerializeField] int dialogueIndex;
    [SerializeField] int currentLineIndex;
    DialogueController dialogueController;
    protected override void Start()
    {
        base.Start();
        dialogueController = GameManager.instance.dialogueController;
    }
    public override void Interact()
    {
        bool hasDialogue = dialogueIndex < dialogues.Length;
        if (hasDialogue)
           WriteLine();
    }
    void WriteLine()
    {
        Dialogue currentDialogue = dialogues[dialogueIndex];
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            dialogueController.WriteDialogue(currentDialogue.lines[currentLineIndex], currentDialogue.npcName);
            currentLineIndex++;
        }
        else
        {
            dialogueIndex++;

            if (loopDialogueOnEnd && dialogueIndex >= dialogues.Length)
                dialogueIndex = 0;

            dialogueController.SetActive(false);
            currentLineIndex = 0;
        }
    }
    #region OnRaycast
    public override void OnRaycastEnter()
    {
        base.OnRaycastEnter();
    }
    public override void OnRaycastExit()
    {
        base.OnRaycastExit();
    }
    #endregion
}
