using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
	[SerializeField]
	private float lookSensitivity;
	[SerializeField]
	private float smoothing;

	private GameObject player;
	private Vector2 smoothedVelocity;
	private Vector2 currentLookingPos;
	private DialogueManager dialogueManager;
	private bool canMove = true;

	void Start()
	{
		player = transform.parent.gameObject;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		SubscribeToEvents();

	}

	private void SubscribeToEvents()
	{
		FindObjectOfType<BuildManager>().OnBuilding += OnBuild;
		FindObjectOfType<DialogueManager>().Chatting += PlayerCameraController_Chatting;
	}

	private void OnBuild(bool isBuilding)
	{
		Debug.Log($"OnBuilding in PlayerCamera with value {isBuilding}");
		canMove = !isBuilding;
		LockControls(canMove);
	}

	private void PlayerCameraController_Chatting(bool isChatting)
	{
		canMove = !isChatting;
		LockControls(canMove);
	}

	private void LockControls(bool shouldLock)
	{
		if (shouldLock)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void Update()
	{
		if (canMove)
		{
			RotateCamera();
		}
	}

	private void CheckForShooting()
	{
		if (Input.GetMouseButtonDown(0))
		{

			RaycastHit whatIhit;
			if (Physics.Raycast(transform.position, transform.forward, out whatIhit, Mathf.Infinity))
			{
				Debug.Log(whatIhit.collider.name);
			}
		}
	}

	private void RotateCamera()
	{
		var x = Input.GetAxisRaw("Mouse X");
		var y = Input.GetAxisRaw("Mouse Y");

		Vector2 inputValues = new Vector2(x, y);

		// To have different vertical and horizontal smoothing then these values would be different in the next line
		inputValues = Vector2.Scale(inputValues, new Vector2(lookSensitivity * smoothing, lookSensitivity * smoothing));

		smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
		smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

		currentLookingPos += smoothedVelocity;

		transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, player.transform.up);
	}
}
