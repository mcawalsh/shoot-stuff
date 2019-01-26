using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
	internal Vector3[] GenerateMap(int floors)
	{
		Debug.Log("Generating Map...");

		var locList = new List<Vector3>();

		// Spawn
		locList.Add(new Vector3(0, 0, 0));

		for (int i = 0; i < floors; i++)
		{
			// x, y, z
			locList.Add(new Vector3(Random.Range(0f, 100f), 0, Random.Range(0f, 100f)));
		}

		return locList.ToArray();
	}
}
