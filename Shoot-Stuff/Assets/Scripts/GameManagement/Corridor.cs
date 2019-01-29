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

		if (gridPosition != null)
			return new GridPosition(gridPosition.X, gridPosition.Y - 1);

		return new GridPosition(0, 0);
	}
}