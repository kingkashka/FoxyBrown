using Godot;
using System;

public partial class LevelBase : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("test"))
		{
			SignalManager.EmitOnCreateObject(new Vector2(200, -100), (int)GameObjectType.Explosion);
		}
	}
}
