using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{

	public class Room : IGridItem
	{
		public Guid Id { get; private set; }
		public RoomType RoomType { get; private set; }
		public Direction Entrance { get; private set; }
		public Door[] Doors; // This is just the direction of the doors, not any connection to the actual hallway

		// Currently working off idea of 1x1 room sizes, in future that needs to change
		// This would be the square space available for room generation (assuming to not always build perfectly square rooms)
		//public int spaceHeight = 1;
		//public int spaceWidth = 1;

		public Vector3 WorldPosition { get; private set; }
		public List<GridPosition> GridPositions { get; set; }

		public Room(RoomType type, Door[] doors, List<GridPosition> gridPositions, Vector3 worldPosition)
		{
			this.Id = Guid.NewGuid();
			this.RoomType = type;
			this.Doors = doors;
			this.WorldPosition = worldPosition;
			this.GridPositions = gridPositions;
		}

		internal GridPosition GetExitGridPosition(Direction south)
		{
			GridPosition gridPosition = GridPositions.First();

			if (gridPosition != null)
				return new GridPosition(gridPosition.X, gridPosition.Y - 1);

			return new GridPosition(0, 0);
		}

		public Room(RoomType type, Door[] doors, Direction entrance, List<GridPosition> gridPositions, Vector3 worldPosition) : this(type, doors, gridPositions, worldPosition)
		{
			this.Entrance = entrance;
		}
	}
}
