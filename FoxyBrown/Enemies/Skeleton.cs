using Godot;
using System;

public partial class Skeleton : EnemyBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		IdleState();
		FlipMe();
		MoveAndSlide();

		// Add your own functionality here.
	}

	private void IdleState()
	{
		Velocity = new Vector2(0, 0);
		animatedSprite.Play("Idle");
	}
	private void FlipMe()
	{
		animatedSprite.FlipH = playerReference.GlobalPosition.X > GlobalPosition.X;
	}
}
