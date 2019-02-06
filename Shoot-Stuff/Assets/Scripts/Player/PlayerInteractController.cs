using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
	private Interactable focussedItem;

	void OnTriggerEnter(Collider collider)
	{
		Interactable interactable = collider.GetComponent<Interactable>();

		if (interactable != null)
		{
			focussedItem = interactable;
			focussedItem.TriggerInteraction();
		}
	}

	void OnTriggerExit(Collider collider)
	{
		Interactable interactable = collider.GetComponent<Interactable>();

		if (interactable != null)
		{
			if (interactable == focussedItem)
			{
				focussedItem.EndInteraction();
				focussedItem = null;
			}
		}
	}

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			//Debug.Log("Mouse is down");

			//RaycastHit whatIhit;
			//if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out whatIhit, Mathf.Infinity))
			//{
			//	Debug.Log($"I have hit {whatIhit.collider.name}");

			//	Interactable interactable = whatIhit.collider.GetComponent<Interactable>();
			//	if (interactable != null)
			//	{
			//		Debug.Log("setting focus...");
			//		//interactable.OnFocused(fpsCam.transform);
			//	}
			//}
		}
	}
}
