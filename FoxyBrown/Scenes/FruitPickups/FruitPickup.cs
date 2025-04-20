using Godot;
using System;

public partial class FruitPickup : Area2D
{
	[Export] private AnimatedSprite2D animatedSprite;
	[Export] private Timer timer;
	[Export] private float jump = -80.0f;
	
	private float startY;
	private float speedY;
	private bool stopped = false;

	private int points = 2;
	public override void _Ready()
	{
		speedY = jump;
		startY = Position.Y;
		AreaEntered += OnAreaEntered;
		timer.Timeout += OnTimerTimeout;
		PlayRandomAnimation();
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.

	private void PlayRandomAnimation()
	{
		var animNames = animatedSprite.SpriteFrames.GetAnimationNames();

		if(animNames.Length > 0)
		{
			string randomAnim = animNames[GD.Randi() % animNames.Length];
			animatedSprite.Play(randomAnim);
		}
	}
    public override void _Process(double delta)
	{
		if(stopped)
		{
			return;
		}
		Position += new Vector2(0, speedY * (float)delta);
		speedY += Gravity * (float)delta; // Gravity

		if(Position.Y > startY)
		{
			stopped = true;

		}
	}
	private void OnTimerTimeout()
    {
       QueueFree(); // Remove the fruit after the timer times out
    }
	private void OnAreaEntered(Area2D area)
    {
        QueueFree(); // Remove the fruit when it enters the player's area
		SignalManager.EmitOnPickupHit(points);
    }
}
