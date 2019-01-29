using Assets.Scripts.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private DungeonGenerator generator;

	public GameObject RoomGameObject;
	public GameObject TopLeft;
	public GameObject Player;
	public int GridWidth = 10;
	public int GridHeight = 10;
	public int GridRatio = 4;
	public int MaxRoomWidth = 1;
	public int MaxRoomHeight = 1;
	public int MaxBranchDepth = 5;

	void Start()
	{
		Debug.Log("GameStateManager Starting...");

		if (generator == null)
			generator = new DungeonGenerator(GridRatio, GridWidth, GridHeight, MaxRoomWidth, MaxRoomHeight, MaxBranchDepth);

		Dungeon dungeon = generator.GenerateDungeon();

		DrawDungeon(dungeon);
		PlacePlayer(dungeon.Spawn);
	}

	private void PlacePlayer(Room spawn)
	{
		if (Player != null)
		{
			var worldPos = spawn.GetWorldPositions(GridRatio).First();
			Debug.Log($"Placing player at {worldPos}...");
			Player.transform.position = new Vector3(worldPos.x, 1.5f, worldPos.z);
		}
	}

	private void DrawDungeon(Dungeon dungeon)
	{
		foreach (KeyValuePair<Guid, Room> kvp in dungeon.Rooms)
		{
			Debug.Log($"Drawing room {kvp.Value.Id}...");
			foreach(var worldPos in kvp.Value.GetWorldPositions(GridRatio))
				Instantiate(RoomGameObject, worldPos, Quaternion.identity);
		}

		foreach (KeyValuePair<Guid, Corridor> kvp in dungeon.Corridors)
		{
			foreach(var worldPos in kvp.Value.GetWorldPositions(GridRatio))
				Instantiate(TopLeft, worldPos, Quaternion.identity);
		}
	}

	private GameObject GetCorridorGameObject(CorridorType type)
	{
		switch (type)
		{
			default:
				return TopLeft;
		}
	}

}
