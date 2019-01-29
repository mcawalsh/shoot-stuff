using Assets.Scripts.GameManagement;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridItem
{
	public List<GridPosition> GridPositions { get; protected set; }

	public List<Vector3> GetWorldPositions(int ratio)
	{
		List<Vector3> positions = new List<Vector3>();

		foreach (var pos in GridPositions)
			positions.Add(new Vector3(pos.X * ratio, 0, pos.Y * ratio));

		return positions;
	}
}
