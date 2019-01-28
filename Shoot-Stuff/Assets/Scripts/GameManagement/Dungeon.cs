using System;
using System.Collections.Generic;
using Assets.Scripts.GameManagement;

public class Dungeon
{
	public Dictionary<Guid, Room> Rooms { get; private set; }
	public Dictionary<Guid, Corridor> Corridors { get; private set; }
	public Room Spawn { get; set; }
	public Room Final { get; set; }

	public Dungeon()
	{
		this.Rooms = new Dictionary<Guid, Room>();
		this.Corridors = new Dictionary<Guid, Corridor>();
	}
}