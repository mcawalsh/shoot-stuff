using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameManagement;
using UnityEngine;
using Random = System.Random;
using URandom = UnityEngine.Random;

public class DungeonGenerator
{
	private Dungeon dungeon;
	private int gridX, gridY, gridRatio;
	private int maxRoomX, maxRoomY;
	private int maxBranchDepth;
	private IGridItem[,] roomsArray;
	private List<Direction> directionOptions = new List<Direction>() { Direction.East, Direction.North, Direction.South, Direction.West };

	public DungeonGenerator(int gridRatio, int gridX, int gridY, int maxRoomX, int maxRoomY, int maxBranchDepth)
	{
		this.gridRatio = gridRatio;
		this.gridX = gridX;
		this.gridY = gridY;
		this.maxRoomX = maxRoomX;
		this.maxRoomY = maxRoomY;
		this.maxBranchDepth = maxBranchDepth;
		this.dungeon = new Dungeon();
		roomsArray = new IGridItem[this.gridX * 2, this.gridY * 2];
	}

	public Dungeon GenerateDungeon()
	{
		// Create the Spawn Room
		dungeon.Spawn = GenerateRoom(RoomType.Spawn, new List<GridPosition>() { new GridPosition(gridX, gridY) }, -1);

		// Add corridor south of Spawn Room
		List<GridPosition> corridorGridPositions = new List<GridPosition>() { dungeon.Spawn.GetExitGridPosition(Direction.South) };
		Corridor corridor = new Corridor(CorridorType.TopRight, corridorGridPositions);
		this.dungeon.Corridors.Add(corridor.Id, corridor);

		AddGridItemToArray(corridor);

		GenerateRoom(RoomType.Standard, new List<GridPosition>() { corridor.GetExitGridPosition(Direction.South) }, 0);

		return dungeon;
	}

	private Room GenerateRoom(RoomType roomType, List<GridPosition> gridPositions, int branchDepth)
	{
		List<Door> roomDoors = GenerateDoors(roomType, gridPositions, new List<Direction>());
		Room newRoom = new Room(roomType, roomDoors, gridPositions);

		this.dungeon.Rooms.Add(newRoom.Id, newRoom);
		AddGridItemToArray(newRoom);

		if (roomType == RoomType.Standard)
		{
			if (branchDepth >= this.maxBranchDepth)
			{
				newRoom.ClearDoors();
			}
			else
			{
				GenerateBranches(roomType, newRoom, branchDepth + 1);
			}
		}

		return newRoom;
	}

	private bool GenerateBranches(RoomType roomType, Room currentRoom, int branchDepth)
	{
		List<Door> doorsToRemove = new List<Door>();

		// For each door go generate a Branch
		foreach (Door door in currentRoom.Doors)
		{
			if (!GenerateBranchedCorridors(CorridorType.Straight, door.GetExitPosition(), branchDepth))
			{
				doorsToRemove.Add(door);
			}
		}

		currentRoom.RemoveDoors(doorsToRemove);

		return true;
		// Remove the doors
	}

	private bool GenerateBranchedCorridors(CorridorType corridorType, GridPosition newItemGridPosition, int branchDepth)
	{
		// Check if the grid position is valid, if it isn't then return false
		if (this.roomsArray[newItemGridPosition.X, newItemGridPosition.Y] != null)
			return false; // This means something already exists there

		List<GridPosition> corridorGridPositions = new List<GridPosition>() { newItemGridPosition };
		Corridor corridor = new Corridor(CorridorType.TopRight, corridorGridPositions);

		if (!GenerateBranchedRooms(RoomType.Standard, corridor.GetExitGridPosition(Direction.South), branchDepth + 1)) {
			return false;
		}

		this.dungeon.Corridors.Add(corridor.Id, corridor);
		AddGridItemToArray(corridor);

		return true;
	}

	private bool GenerateBranchedRooms(RoomType roomType, GridPosition newItemGridPosition, int branchDepth) {

		// Check if the grid position is valid, if it isn't then return false
		if (this.roomsArray[newItemGridPosition.X, newItemGridPosition.Y] != null)
			return false; // This means something already exists there

		// Create the room
		List<GridPosition> newGridPositions = new List<GridPosition>() { newItemGridPosition };
		List<Door> roomDoors = GenerateDoors(roomType, newGridPositions, new List<Direction>());
		Room newRoom = new Room(roomType, roomDoors, newGridPositions);

		// If the branchDepth <= max depth generate branches
		if (branchDepth >= this.maxBranchDepth)
		{
			newRoom.ClearDoors();
		}
		else
		{
			GenerateBranches(roomType, newRoom, branchDepth + 1);
		}

		this.dungeon.Rooms.Add(newRoom.Id, newRoom);
		AddGridItemToArray(newRoom);

		return true;
	}

	private Corridor GenerateCorridorBranch(Door door, int branchDepth)
	{
		List<GridPosition> corridorGridPositions = new List<GridPosition>() { door.GetExitPosition() };
		Corridor corridor = new Corridor(CorridorType.TopRight, corridorGridPositions);
		this.dungeon.Corridors.Add(corridor.Id, corridor);
		AddGridItemToArray(corridor);

		return corridor;
	}

	private Corridor GenerateCorridor(Door door)
	{
		List<GridPosition> corridorGridPositions = new List<GridPosition>() { door.GetExitPosition() };
		Corridor corridor = new Corridor(CorridorType.TopRight, corridorGridPositions);
		this.dungeon.Corridors.Add(corridor.Id, corridor);
		AddGridItemToArray(corridor);
		return corridor;
	}

	private List<Door> GenerateDoors(RoomType roomType, List<GridPosition> roomPositions, List<Direction> unavailableDoors)
	{
		if (roomType == RoomType.Spawn)
			return new List<Door> { new Door(Direction.South, roomPositions.First()) };
		else if (roomType == RoomType.Final)
			return new List<Door>();

		Random rnd = new Random();

		List<GridPosition> viableExits = GetViableExits(roomPositions).OrderBy(x => rnd.Next()).ToList();
		List<Direction> doorOptions = directionOptions.Except(unavailableDoors).OrderBy(x => rnd.Next()).ToList();
		int numOfDoors = URandom.Range(0, doorOptions.Count);

		List<Door> doors = new List<Door>();

		for (int i = 0; i < numOfDoors; i++)
		{
			Direction currentDirection = doorOptions.First();
			GridPosition currentPosition = viableExits.First();
			doors.Add(new Door(currentDirection, currentPosition));

			// Remove the used direction and exit
			doorOptions.Remove(currentDirection);
			viableExits.Remove(currentPosition);
		}

		return doors;
	}

	private List<GridPosition> GetViableExits(List<GridPosition> roomPositions)
	{
		List<GridPosition> exits = new List<GridPosition>();

		foreach (var pos in roomPositions)
		{
			// North
			var north = new GridPosition(pos.X, pos.Y + 1);
			exits.Add(north);
			// East
			var east = new GridPosition(pos.X + 1, pos.Y);
			exits.Add(east);
			// West
			var west = new GridPosition(pos.X - 1, pos.Y + 1);
			exits.Add(west);
			// South
			var south = new GridPosition(pos.X, pos.Y - 1);
			exits.Add(south);
		}

		return exits;
	}

	private void AddGridItemToArray(IGridItem gridItem)
	{
		foreach (var item in gridItem.GridPositions)
			roomsArray[item.X, item.Y] = gridItem;
	}

}
