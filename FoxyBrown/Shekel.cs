using Godot;
using System;

public partial class Shekel : Area2D
{
	[Export] private AudioStreamPlayer2D ShekelSound;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
    {
        if(body is Player player)
		{
			// player.AddShekel();
			ShekelSound.Play();
			QueueFree(); // Remove the shekel after collection
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
