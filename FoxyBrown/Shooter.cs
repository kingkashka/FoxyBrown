using Godot;
using System;

public partial class Shooter : Node2D
{
	[Export] private float speed = 5000.0f;
	[Export] private float lifeSpan = 10.0f;
	[Export] private GameObjectType bulletKey;
	[Export] private float shootDelay = 0.2f;

	[Export] private AudioStreamPlayer2D sound;
	[Export] private Timer shootTimer;

	private bool canShoot = true;
	public override void _Ready()
	{
		shootTimer.Timeout += () =>
		{
			canShoot = true;
			// shootTimer.Stop();
		};	
	}

	public void Shoot(Vector2 direction)
	{
		if(canShoot)
		{
			canShoot = false;
			SignalManager.EmitOnCreateBullet(GlobalPosition, direction, speed, lifeSpan, (int)bulletKey);
			SoundManager.PlayClip(sound, SoundManager.SOUND_LASER);
			shootTimer.Start(shootDelay);
		}
		else
		{
			return;
		}
	}
}
