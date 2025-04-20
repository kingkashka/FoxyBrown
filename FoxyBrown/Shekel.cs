using Godot;
using System;

public partial class Shekel : Area2D
{
	[Export] private AudioStreamPlayer2D ShekelSound;
	[Export] private Timer ShekelTimer;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		ShekelTimer.Timeout += OnShekelTimerTimeout;
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

    public override void _Process(double delta)
	{
	}
}
