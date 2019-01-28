using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
	public class Room
	{
		public Guid Id { get; private set; }
		public RoomType RoomType { get; private set; }
		public Direction Entrance { get; private set; }
		public Door[] Doors; // This is just the direction of the doors, not any connection to the actual hallway
		public Vector3 WorldPosition { get; private set; }

		// Currently working off idea of 1x1 room sizes, in future that needs to change
		// This would be the square space available for room generation (assuming to not always build perfectly square rooms)
		//public int spaceHeight = 1;
		//public int spaceWidth = 1;

		public Vector3 Position { get; set; }
		public List<GridPosition> GridPositions { get; set; }

		public Room(RoomType type, Door[] doors, Vector3 worldPosition)
		{
			this.Id = Guid.NewGuid();
			this.RoomType = type;
			this.Doors = doors;
			this.WorldPosition = worldPosition;
		}

		public Room(RoomType type, Door[] doors, Direction entrance, Vector3 worldPosition) : this(type, doors, worldPosition)
		{
			this.Entrance = entrance;
		}
	}
}
