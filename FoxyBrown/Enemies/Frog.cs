using Godot;
using System;

public partial class Frog : EnemyBase
{
	[Export] private RayCast2D floorRayCast;
	[Export] private Timer jumpTimer;
	private bool seenPlayer = false;
	
	public enum FrogState
	{
		Idle,
		Jumping
	}

	FrogState currentState = FrogState.Idle;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	private void UpdateState()
	{

	}
	private void StateDetection()
	{

	}
	protected override void OnScreenEntered()
	{
		if(!seenPlayer)
		{
		seenPlayer = true;
		GD.Print("Frog has seen the player");
		}
	}

}
