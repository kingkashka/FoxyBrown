using Godot;
using System;

public partial class Snail : EnemyBase
{

	[Export] private RayCast2D floorRayCast;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		Vector2 velocity = Velocity;

		if(!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}
		else
		{
			velocity.X = animatedSprite.FlipH ? speed : -speed;
		}
		
		Velocity = velocity;
		MoveAndSlide();
		FlipMe();
	}

	private void FlipMe()
	{
		if(IsOnFloor() && IsOnWall() || !floorRayCast.IsColliding())
		{
			animatedSprite.FlipH = !animatedSprite.FlipH;
			floorRayCast.Position = new Vector2(floorRayCast.Position.X * -1, floorRayCast.Position.Y);
		}
	}
}
