using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private MapGenerator mapGenerator;
	private GameObject[] floors;

	[SerializeField]
	public GameObject floorPrefab;
	[SerializeField]
	public int rooms = 2;

    void Start()
    {
		Debug.Log("GameStateManager Starting...");

		if (mapGenerator == null)
			mapGenerator = new MapGenerator();

		var positions = mapGenerator.GenerateMap(rooms);

		BuildMap(positions);
		
    }

	private void BuildMap(Vector3[] positions)
	{
		floors = new GameObject[rooms];

		for (int i = 0; i < positions.Length; i++)
		{
			var pos = positions[i];

			var newFloor = Instantiate(floorPrefab, pos, Quaternion.identity);
			floors[i] = newFloor;
		}
	}

    
}
