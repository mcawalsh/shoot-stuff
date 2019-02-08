using UnityEngine;

public delegate void BuildingEvent(bool isBuilding);

public class BuildManager : MonoBehaviour
{
	public GameObject buildPanel;
	public Camera fpsCamera;
	public DialogueManager dialogueManager;
	public BuildingEvent OnBuilding;

	void Start()
	{

	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.B))
		{
			SwapGamePanel();
		}
    }

	private void SwapGamePanel()
	{
		// if active then building
		bool isBuilding = buildPanel.activeSelf;

		// Swap it
		buildPanel.SetActive(!isBuilding);

		Debug.Log($"Publishing onBuild as {!isBuilding}");
		this.OnBuilding(!isBuilding);
	}

	public void BuildVendingMachine(GameObject item)
	{
		SwapGamePanel();

		var build = Instantiate(item, fpsCamera.transform.position + (Vector3.forward * 10), Quaternion.identity);
		var renderer = build.GetComponent<Renderer>();
		

		var col = renderer.material.color;
		col.a = 0.5f;

		renderer.material.SetColor("_Color", col);
	}
}
