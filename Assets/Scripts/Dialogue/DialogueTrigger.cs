using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activates a specified conversation when the player walks into a trigger.
public class DialogueTrigger : MonoBehaviour
{
    //A different conversation is used depending on if the game is in story mode
    [SerializeField]
    private Conversation standardConversation;

    public void TriggerDialogue()
    {
        if (!DialogueManager.Instance.inConversation)
        {
            DialogueManager.Instance.StartConversation(standardConversation);
        }
    }
}
