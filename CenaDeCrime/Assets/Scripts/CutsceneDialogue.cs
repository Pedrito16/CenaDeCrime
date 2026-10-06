using UnityEngine;
using UnityEngine.InputSystem;
public class CutsceneDialogue : MonoBehaviour
{
    [SerializeField] Dialogue[] dialogues;
    [SerializeField] int currentDialogueIndex = 0;
    [SerializeField] int currentLineIndex = 0;
    public bool endedDialogues;
    DialogueController dialogueController;
    void Start()
    {
        dialogueController = GameManager.instance.dialogueController;
        dialogueController.WriteDialogue(dialogues[0].lines[0],  dialogues[0].npcName);
    }
    public void OnPassDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PassDialogue();
        }
    }
    public void PassDialogue()
    {
        CheckIfEndedDialogue();
        string[] currentLine = dialogues[currentDialogueIndex].lines;
        if (dialogueController.isOnDialogue)
        {
            dialogueController.ResetText();
            return;
        }
        
        if (currentLineIndex < currentLine.Length - 1)
        {
            print("nao acabou lines");
            currentLineIndex++;
            dialogueController.WriteDialogue(currentLine[currentLineIndex], dialogues[currentDialogueIndex].npcName);
        }
        else
        {
            print("acabou lines, passando pro proximo");
            dialogues[currentDialogueIndex].onLinesEnd?.Invoke();
            currentDialogueIndex++;
            currentLineIndex = 0;
            CheckIfEndedDialogue();
            if (currentDialogueIndex < dialogues.Length)
            {
                dialogueController.WriteDialogue(dialogues[currentDialogueIndex].lines[currentLineIndex], dialogues[currentDialogueIndex].npcName);
            }
        }
    }
    public void CheckIfEndedDialogue()
    {
        if (currentDialogueIndex > dialogues.Length - 1)
        {
            print("acabou dialogos");
            endedDialogues = true;
            dialogueController.nextSelect.gameObject.SetActive(false);
            return;
        }
    }
}