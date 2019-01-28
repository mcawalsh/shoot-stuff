using Assets.Scripts.GameManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : IGridItem
{
	public Guid Id { get; private set; }
	public CorridorType Type { get; private set; }
	public Vector3 WorldPosition { get; private set; }
	public List<GridPosition> GridPositions { get; private set; }

	public Corridor(CorridorType type, List<GridPosition> gridPositions, Vector3 worldPosition)
	{
		this.Id = Guid.NewGuid();
		this.Type = type;
		this.WorldPosition = worldPosition;
		this.GridPositions = gridPositions;
	}
}