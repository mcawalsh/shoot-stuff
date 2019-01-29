using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameManagement
{

	public class Room : GridItem, IGridItem
	{
		public Guid Id { get; private set; }
		public RoomType RoomType { get; private set; }
		public Direction Entrance { get; private set; }
		public List<Door> Doors; // This is just the direction of the doors, not any connection to the actual hallway

		public Room(RoomType type, List<Door> doors, List<GridPosition> gridPositions)
		{
			this.Id = Guid.NewGuid();
			this.RoomType = type;
			this.Doors = doors;
			this.GridPositions = gridPositions;
		}

		public Room(RoomType type, List<Door> doors, Direction entrance, List<GridPosition> gridPositions) : this(type, doors, gridPositions)
		{
			this.Entrance = entrance;
		}

		public GridPosition GetExitGridPosition(Direction south)
		{
			GridPosition gridPosition = GridPositions.First();

			if (gridPosition != null)
				return new GridPosition(gridPosition.X, gridPosition.Y - 1);

			return new GridPosition(0, 0);
		}

		public void ClearDoors()
		{
			this.Doors.Clear();
		}

		internal void RemoveDoors(List<Door> doorsToRemove)
		{
			this.Doors = this.Doors.Except(doorsToRemove).ToList();
		}
	}
}
