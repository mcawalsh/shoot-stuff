namespace Assets.Scripts.GameManagement
{
	public class Door
	{
		public Direction Direction { get; private set; }
		public GridPosition GridPosition { get; private set; }

		public Door(Direction direction, GridPosition gridPosition)
		{
			this.Direction = direction;
			this.GridPosition = gridPosition;
		}

		public Door(Direction direction, int x, int y) : this(direction, new GridPosition(x, y)) { }
	}
}