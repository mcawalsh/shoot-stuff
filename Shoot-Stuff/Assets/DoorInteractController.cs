using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractController : MonoBehaviour
{
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log($"Triggered by {collider.name}");

		animator.SetBool("character_nearby", true);
	}

	void OnTriggerExit(Collider collider)
	{
		animator.SetBool("character_nearby", false);
	}
}
