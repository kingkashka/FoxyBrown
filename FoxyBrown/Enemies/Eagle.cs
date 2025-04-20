using Godot;
using System;

public partial class Eagle : EnemyBase
{
	[Export] private Timer directionTimer;
	[Export] private RayCast2D PlayerDetectRayCast;
	[Export] private Shooter shooterNode;
	private readonly Vector2 FlySpeed = new Vector2(35, 15);
	private Vector2 FlyDirection = Vector2.Zero;
	
	public override void _Ready()
	{
		base._Ready();
		directionTimer.Timeout += OnDirectionTimerTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Velocity = FlyDirection;
		MoveAndSlide();
		EagleShoot();
	}
	private void SetDirectionAndFlip()
	{
		int xDirection = Math.Sign(playerReference.GlobalPosition.X - GlobalPosition.X);
		animatedSprite.FlipH = xDirection > 0;
		FlyDirection = new Vector2(xDirection, 1) * FlySpeed;
	}
	private void FlyToPlayer()
	{
		SetDirectionAndFlip();
		directionTimer.Start();
	}
	private void OnDirectionTimerTimeout()
	{
		FlyToPlayer();
	}
	protected override void OnScreenEntered()
	{
		animatedSprite.Play("Fly");
		FlyToPlayer();
	}
	private void EagleShoot()
	{
		if(PlayerDetectRayCast.IsColliding())
		{
			shooterNode.Shoot(Vector2.Down);	
		}	
		else
		{
			return;
		}
	}
}
