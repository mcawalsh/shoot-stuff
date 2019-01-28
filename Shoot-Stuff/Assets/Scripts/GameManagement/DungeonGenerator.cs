using Assets.Scripts.GameManagement;
using UnityEngine;

public class DungeonGenerator
{
	private Dungeon dungeon;
	private int gridX, gridY, gridRatio;
	private Room[,] roomsArray;

	public DungeonGenerator(int gridRatio, int gridX, int gridY)
	{
		this.gridRatio = gridRatio;
		this.gridX = gridX;
		this.gridY = gridY;
		this.dungeon = new Dungeon();
		roomsArray = new Room[this.gridX * 2, this.gridY * 2];
	}

	public Dungeon GenerateDungeon()
	{
		// Create the Spawn Room
		Door[] spawnDoors = new Door[1] { new Door(Direction.South, gridX, gridY) };
		Vector3 worldPosition = GetWorldPosition(gridX, gridY);
		dungeon.Spawn = new Room(RoomType.Spawn, spawnDoors, worldPosition);
		this.dungeon.Rooms.Add(dungeon.Spawn.Id, dungeon.Spawn);

		// Place at middle of the grid
		roomsArray[this.gridX, this.gridY] = dungeon.Spawn;

		return dungeon;
	}

	private Vector3 GetWorldPosition(int gridX, int gridY)
	{
		return new Vector3(gridX * gridRatio, 0, gridY * gridRatio);
	}
}