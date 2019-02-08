using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

	[SerializeField]
	private float speed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
	private float raycastDistance;

	private Rigidbody rb;
	private bool canMove = true;


	void Start()
	{
		rb = GetComponent<Rigidbody>();

		SubscribeToEvents();
	}

	private void SubscribeToEvents()
	{
		FindObjectOfType<DialogueManager>().Chatting += PlayerCameraController_Chatting;
		FindObjectOfType<BuildManager>().OnBuilding += OnBuilding;
	}

	private void OnBuilding(bool isBuilding)
	{
		Debug.Log($"OnBuilding in PlayerMovement with value {isBuilding}");
		canMove = !isBuilding;
	}

	private void PlayerCameraController_Chatting(bool isChatting)
	{
		canMove = !isChatting;
	}

	void Update()
	{
		if (canMove)
		{
			Jump();
		}
	}

	private void FixedUpdate()
	{
		if (canMove)
		{
			Move();
		}
	}

	private void Move()
	{
		float hAxis = Input.GetAxisRaw("Horizontal");
		float vAxis = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.fixedDeltaTime;

		// This will make the character move in the movement direction relative to the direction
		// the character is facing
		Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

		rb.MovePosition(newPosition);
	}

	private void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (IsGrounded())
			{
				rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
			}
		}
	}

	private bool IsGrounded()
	{
		Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.blue);
		return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
	}
}
