using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
	public interface IGridItem
	{
		Guid Id { get; }
		List<Vector3> GetWorldPositions(int ratio);
		List<GridPosition> GridPositions { get; }
	}
}
