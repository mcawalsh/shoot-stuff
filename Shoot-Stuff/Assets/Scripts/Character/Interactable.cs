using UnityEngine;

public class Interactable : MonoBehaviour
{
	public InteractionText text;
	private DialogueManager dialogueManager;

	void Start()
	{
		dialogueManager = FindObjectOfType<DialogueManager>();
	}

	public void TriggerInteraction()
	{
		dialogueManager.StartInteraction(text);
	}

	public void EndInteraction()
	{
		dialogueManager.EndInteraction();
	}

	//void OnTriggerEnter(Collider collider)
	//{
	//	float distance = Vector3.Distance(collider.transform.position, transform.position);

	//	if (distance < radius)
	//	{
	//		Debug.Log("triggered...");
	//	}
	//}

	//void OnDrawGizmosSelected()
	//{
	//	Gizmos.color = Color.yellow;
	//	Gizmos.DrawWireSphere(transform.position, radius);
	//}

	//bool isFocus = false;
	//Transform player;

	//public void OnFocused(Transform playerTransform)
	//{
	//	isFocus = true;
	//	player = playerTransform;
	//}

	//public void OnDefocused()
	//{
	//	player = null;
	//}

	//void Update()
	//{
	//	if (isFocus)
	//	{
	//		float distance = Vector3.Distance(player.position, transform.position);

	//		if (distance <= radius)
	//		{
	//			Debug.Log("interacting...");
	//		}
	//	}
	//}
}
