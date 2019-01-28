using Assets.Scripts.GameManagement;
using System;
using System.Collections.Generic;
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

	void Start()
	{
		Debug.Log("GameStateManager Starting...");

		if (generator == null)
			generator = new DungeonGenerator(GridRatio, GridWidth, GridHeight);

		Dungeon dungeon = generator.GenerateDungeon();

		DrawDungeon(dungeon);
		PlacePlayer(dungeon.Spawn);
	}

	private void PlacePlayer(Room spawn)
	{
		if (Player != null)
		{
			Debug.Log($"Placing player at {spawn.WorldPosition}...");
			Player.transform.position = new Vector3(spawn.WorldPosition.x, 1.5f, spawn.WorldPosition.z);
		}
	}

	private void DrawDungeon(Dungeon dungeon)
	{
		foreach (KeyValuePair<Guid, Room> kvp in dungeon.Rooms)
		{
			Debug.Log($"Drawing room {kvp.Value.Id}...");
			Instantiate(RoomGameObject, kvp.Value.WorldPosition, Quaternion.identity);
		}

		foreach (KeyValuePair<Guid, Corridor> kvp in dungeon.Corridors)
		{
			GameObject go = GetCorridorGameObject(kvp.Value.Type);
			Instantiate(go, kvp.Value.WorldPosition, Quaternion.identity);
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
