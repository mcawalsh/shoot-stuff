using Assets.Scripts.GameManagement;
using System;
using System.Collections.Generic;
using System.Linq;

public class Corridor : GridItem, IGridItem
{
	public Guid Id { get; private set; }
	public CorridorType Type { get; private set; }

	public Corridor(CorridorType type, List<GridPosition> gridPositions)
	{
		this.Id = Guid.NewGuid();
		this.Type = type;
		this.GridPositions = gridPositions;
	}

	public GridPosition GetExitGridPosition(Direction direction)
	{
		GridPosition gridPosition = GridPositions.First();

		switch(direction)
		{
			case Direction.North:
				return new GridPosition(gridPosition.X, gridPosition.Y + 1);
			case Direction.East:
				return new GridPosition(gridPosition.X + 1, gridPosition.Y);
			case Direction.West:
				return new GridPosition(gridPosition.X - 1, gridPosition.Y);
			default:
				return new GridPosition(gridPosition.X, gridPosition.Y - 1);
		}
	}
}