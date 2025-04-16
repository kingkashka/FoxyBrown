using Godot;
using System;

public partial class Skeleton : EnemyBase
{
	[Export] private RayCast2D AttackRayCast;
	[Export] private RayCast2D floorRayCast;
	private bool seenPlayer = false;
	[Export] private Timer attackTimer;
	[Export] private Area2D attackArea;

	public override void _Ready()
	{
		base._Ready();
		attackTimer.Timeout += OnAttackTimerTimeout;
		attackArea.BodyEntered += OnAttackAreaEntered;

	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		Velocity = velocity;

		MovementState();
		MoveAndSlide();
		FlipMe();
		OnAttackTimerTimeout();
	}
	private void OnAttackAreaEntered(object body)
	{
		if (body is Player player)
		{
			GD.Print("Player hit by attack area");
			// player.TakeDamage(1);
		}
	}


	private void OnAttackTimerTimeout()
	{
		// attackTimer.WaitTime = 1.0f;
		AttackPlayer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.


	private void AttackPlayer()
	{
		if (AttackRayCast.IsColliding())
		{
			var player = AttackRayCast.GetCollider();
			if (player is Player)
			{
				animatedSprite.Play("Attack");
				GD.Print("Attacking player");
				Velocity = Vector2.Zero;
				attackTimer.Start();

			}
		}
		else
		{
			return;
		}
	}
	private void MovementState()
	{
		if (animatedSprite.Animation == "Attack")
		{
			return;
		}

		if (IsOnFloor())
		{
			Velocity = new Vector2(animatedSprite.FlipH ? -speed : speed, Velocity.Y);
			animatedSprite.Play("Movement");
		}

	}
	private void FlipMe()
	{
		if ((IsOnFloor() && IsOnWall()) || !floorRayCast.IsColliding())
		{
			animatedSprite.FlipH = !animatedSprite.FlipH;
			floorRayCast.Position = new Vector2(floorRayCast.Position.X * -1, floorRayCast.Position.Y);
			AttackRayCast.Position = new Vector2(AttackRayCast.Position.X * -1, AttackRayCast.Position.Y);
		}
	}
	// protected override void OnScreenEntered()
	// {
	// 	if (seenPlayer == false)
	// 	{
	// 		seenPlayer = true;
			
	// 		GD.Print("Skeleton has seen the player");
	// 	}
	// }
}
