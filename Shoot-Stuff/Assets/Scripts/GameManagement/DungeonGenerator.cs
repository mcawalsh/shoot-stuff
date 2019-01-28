using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameManagement;
using UnityEngine;

public class DungeonGenerator
{
	private Dungeon dungeon;
	private int gridX, gridY, gridRatio;
	private IGridItem[,] roomsArray;

	public DungeonGenerator(int gridRatio, int gridX, int gridY)
	{
		this.gridRatio = gridRatio;
		this.gridX = gridX;
		this.gridY = gridY;
		this.dungeon = new Dungeon();
		roomsArray = new IGridItem[this.gridX * 2, this.gridY * 2];
	}

	public Dungeon GenerateDungeon()
	{
		// Create the Spawn Room
		Door[] spawnDoors = new Door[1] { new Door(Direction.South, gridX, gridY) };
		List<GridPosition> spawnGridPositions = new List<GridPosition>() { new GridPosition(gridX, gridY) };
		Vector3 worldPosition = GetWorldPosition(spawnGridPositions);
		dungeon.Spawn = new Room(RoomType.Spawn, spawnDoors, spawnGridPositions, worldPosition);
		this.dungeon.Rooms.Add(dungeon.Spawn.Id, dungeon.Spawn);

		// 
		// roomsArray[this.gridX, this.gridY] = dungeon.Spawn;
		AddGridItemToArray(dungeon.Spawn);

		// Add corridor south of Spawn Room
		List<GridPosition> corridorGridPositions = new List<GridPosition>() { dungeon.Spawn.GetExitGridPosition(Direction.South) };
		Vector3 corridorWorldPosition = GetWorldPosition(corridorGridPositions);
		Corridor corridor = new Corridor(CorridorType.TopRight, corridorGridPositions, corridorWorldPosition);
		this.dungeon.Corridors.Add(corridor.Id, corridor);

		AddGridItemToArray(corridor);
		//roomsArray[corridorGridPosition.X, corridorGridPosition.Y] = corridor;

		return dungeon;
	}

	private void AddGridItemToArray(IGridItem gridItem)
	{
		foreach (var item in gridItem.GridPositions)
			roomsArray[item.X, item.Y] = gridItem;
	}

	private Vector3 ConvertToWorldPosition(GridPosition gridPosition)
	{
		return new Vector3(gridPosition.X * gridRatio, 0, gridPosition.Y * gridRatio);
	}

	private Vector3 GetWorldPosition(List<GridPosition> gridPositions)
	{
		if (gridPositions != null && gridPositions.Count > 0)
		{
			GridPosition pos = gridPositions.First();
			return new Vector3(pos.X * gridRatio, 0, pos.Y * gridRatio);
		}

		// Return middle of the grid
		return new Vector3(gridX * gridRatio, 0, gridY * gridRatio);
	}
}