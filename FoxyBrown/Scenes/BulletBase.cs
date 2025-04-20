using Godot;
using System;

public partial class BulletBase : Area2D
{
	private Vector2 direction = Vector2.Right;
	private double lifeSpan = 20.0f;
	private double lifeTime = 0.0f;
	private float speed = 500.0f;
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
		// Setup(new Vector2(20.0f, -40.0f), 3.0f, 50.0f);
	}

	public override void _PhysicsProcess(double delta)
	{
		checkExpired(delta);
		Position += direction * (float)delta;
		// CoolAnimation();
	}
	public void Setup(Vector2 direction, float lifeSpan, float speed)
	{
		this.direction = direction.Normalized() * speed;
		this.lifeSpan = lifeSpan;
		GD.Print(direction);
	}
	private void checkExpired(double delta)
	{
		lifeTime += delta;
		if(lifeTime > lifeSpan)
		{
			SetProcess(false);
			QueueFree();
		}
	}
	private void OnAreaEntered(Area2D area)
	{
		QueueFree();
	}

	// private void CoolAnimation()
	// {
	// 	Tween BulletSpin = GetTree().CreateTween();

	// 	BulletSpin.TweenCallback(Callable.From(() => GD.Print("Spin")));
	// 	BulletSpin.TweenProperty(this, "rotation_degrees", 360, 2.0f);
	// 	BulletSpin.SetTrans(Tween.TransitionType.Cubic);
	// 	BulletSpin.TweenCallback(Callable.From(CoolAnimation));

	// }
}
