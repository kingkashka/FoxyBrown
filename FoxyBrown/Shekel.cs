using Godot;
using System;

public partial class Shekel : Area2D
{
	[Export] private AudioStreamPlayer2D ShekelSound;
	[Export] private Timer ShekelTimer;

    [Export] private float jump = -80.0f;
    private float startY;
	private float speedY;
    
    private bool stopped = false;

    // Called when the node enters the scene tree for the first time.
    public override void _EnterTree()
    {
        ShekelTimer.Start();
    }

    // Called when the node is added to the scene.

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		ShekelTimer.Timeout += OnShekelTimerTimeout;
        startY = Position.Y;
        speedY = jump;
	}

    private void OnShekelTimerTimeout()
    {
        QueueFree(); // Remove the shekel after the timer times out
    }


    private void OnBodyEntered(Node2D body)
    {
        if(body is Player player)
		{
			// player.AddShekel();
			ShekelSound.Play();
			CallDeferred(MethodName.RemoveShekel);
			
		}
    }

    private void RemoveShekel()
    {
        QueueFree(); // Remove the shekel after collection
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _PhysicsProcess(double delta)
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

    private void OnShekelEnter()
    {
        speedY = jump;
		startY = Position.Y;
    }
}
