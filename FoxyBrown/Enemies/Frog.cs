using Godot;
using System;

public partial class Frog : EnemyBase
{
	[Export] private RayCast2D floorRayCast;
	[Export] private Timer jumpTimer;
	private float jumpForceX = 100f;
	private float jumpForceY = 200f;
	private bool seenPlayer = false;
	private bool canJump = false;
	private float minJumpTime = 2.0f;
	private float maxJumpTime = 3.0f;

	// private static readonly Vector2 jumpForceRight = new Vector2(jumpforceX, -jumpForceY);
	// private static readonly Vector2 jumpForceLeft = new Vector2(-jumpForceX, -jumpForceY);

	public enum FrogState
	{
		Idle,
		Jumping
	}

	FrogState currentState = FrogState.Idle;
	public override void _Ready()
	{
		base._Ready();
		jumpTimer.Timeout += OnJumpTimerTimeout;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		IdleState();
		ApplyJump();
		MoveAndSlide();
		FlipMe();
	}

	// Idle state and gravity physics

	protected override void OnScreenEntered()
	{
		if (seenPlayer == false)
		{
			seenPlayer = true;
			StartTimer();
			GD.Print("Frog has seen the player");
		}
	}
	private void IdleState()
	{
		if (!IsOnFloor())
		{
			Velocity += new Vector2(0, gravity * (float)GetProcessDeltaTime());
		}
		else
		{
			Velocity = new Vector2(0, 0);
			animatedSprite.Play("Idle");
		}
	}
	private void FlipMe()
	{
		if(!IsOnFloor())
		{
			return;
		}
		animatedSprite.FlipH = playerReference.GlobalPosition.X > GlobalPosition.X;
	}
	private void ApplyJump()
	{
		if (IsOnFloor() && seenPlayer && canJump)
		{
			canJump = false;
			animatedSprite.Play("Jump");
			Velocity = animatedSprite.FlipH ? new Vector2(jumpForceX, -jumpForceY) : new Vector2(-jumpForceX, -jumpForceY);
			StartTimer();
		}
	}

	private void StartTimer()
	{
		jumpTimer.WaitTime = GD.RandRange(minJumpTime, maxJumpTime);
		GD.Print("Timer started with ", jumpTimer.WaitTime);
		jumpTimer.Start();
	}
	private void OnJumpTimerTimeout()
	{
		GD.Print("Timer timeout");
		canJump = true;

	}


}
