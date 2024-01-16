using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueString> dialogueStrings = new List<DialogueString>();
    [SerializeField] private Transform npcTransform;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private KeyCode interactionKey = KeyCode.T;


    private bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
 
            if (other.CompareTag("Player") && !hasSpoken)
            {
                other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, npcTransform);
                hasSpoken = true;
            }
    }
}

[System.Serializable]
public class DialogueString
{
    public string text; // Represent the text taht the npc says
    public bool isEnd; // Represent if the line is the final for the converdsation

    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
