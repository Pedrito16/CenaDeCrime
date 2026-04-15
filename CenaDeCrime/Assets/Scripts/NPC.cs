using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    public string npcName;
    [TextArea]
    public string[] dialogues;
}
public class NPC : Interactable
{
    [SerializeField] Dialogue[] dialogues;

    

    [Header("Debug")]
    [SerializeField] int dialogueIndex;
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        
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
