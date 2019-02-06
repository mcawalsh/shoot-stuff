using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This could be an interactables
public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
