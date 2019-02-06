using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void MyDel(bool isChatting);

public class DialogueManager : MonoBehaviour
{
	public Queue<string> sentences;
	public event MyDel Chatting;
	public TextMeshProUGUI nameText;
	public Text dialogueText;

	public GameObject interactionPanel;
	public TextMeshProUGUI interactText;

	public Animator animator;

    void Start()
    {
		sentences = new Queue<string>();
    }

	public void StartDialogue(Dialogue dialogue)
	{
		this.Chatting(true);
		animator.SetBool("IsOpen", true);
		nameText.text = dialogue.name;
		Debug.Log($"Starting conversation with {dialogue.name}");

		sentences.Clear();

		foreach (var sentence in dialogue.sentences)
			sentences.Enqueue(sentence);

		DisplayNextSentence();
	}

	public void StartInteraction(InteractionText text)
	{
		interactText.text = text.sentence;
		interactionPanel.gameObject.SetActive(true);
	}

	public void EndInteraction()
	{
		interactionPanel.gameObject.SetActive(false);
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = string.Empty;

		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	private void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		this.Chatting(false);
	}
}
