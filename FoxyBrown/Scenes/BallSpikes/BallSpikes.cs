using Godot;
using System;

public partial class BallSpikes : PathFollow2D
{
	[Export] private float speed = 25.0f;
	[Export] private float spinSpeed = 400.0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += speed * (float)delta;
		Rotation += spinSpeed * (float)delta;          
	}
}
