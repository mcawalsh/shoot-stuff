namespace Assets.Scripts.GameManagement
{
	public class Door
	{
		public Direction Direction { get; private set; }
		public GridPosition RoomGridPosition { get; private set; }

		public Door(Direction direction, GridPosition gridPosition)
		{
			this.Direction = direction;
			this.RoomGridPosition = gridPosition;
		}

		public Door(Direction direction, int x, int y) : this(direction, new GridPosition(x, y)) { }

		public GridPosition GetExitPosition()
		{
			switch (Direction)
			{
				// TODO: Add extra direction options
				default:
					return new GridPosition(RoomGridPosition.X, RoomGridPosition.Y - 1);
			}
		}
	}
}