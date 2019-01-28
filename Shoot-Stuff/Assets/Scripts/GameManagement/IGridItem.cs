using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManagement
{
	public interface IGridItem
	{
		Guid Id { get; }
		Vector3 WorldPosition { get; }
		List<GridPosition> GridPositions { get; }
	}
}
