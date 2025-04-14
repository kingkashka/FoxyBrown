using Godot;
using System;

[GlobalClass]
public partial class EnemyBase : CharacterBody2D
{
	[Export] private int points = 1;
	[Export] protected float speed = 20.0f;
	[Export] private float yFallOff = 100.0f;
	protected float gravity = 800.0F;

	[Export] protected VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;
	[Export] protected AnimatedSprite2D animatedSprite;

	[Export] protected Area2D hitBox;

	protected static Player playerReference;
	public override void _Ready()
	{
		playerReference = GetTree().GetFirstNodeInGroup("Player") as Player;
		if(playerReference == null)
		{
			GD.PrintErr("Player not found");
		}
		else
		{
			GD.Print("Player found");
		}
		visibleOnScreenNotifier2D.ScreenEntered += OnScreenEntered;
		visibleOnScreenNotifier2D.ScreenExited += OnScreenExited;
		hitBox.AreaEntered += OnHitBoxAreaEntered;
	}
	public override void _PhysicsProcess(double delta)
	{
		FallenOff();
	}

	private void FallenOff()
	{
		if(GlobalPosition.Y > yFallOff)
		{
			QueueFree();
		}
	}
    protected virtual void OnScreenEntered()
    {
       
    }


    protected virtual void OnScreenExited()
    {
       
    }


    private void OnHitBoxAreaEntered(Area2D area)
    {
        Die();
    }

    private void Die()
    {
		SignalManager.EmitOnEnemyHit(points, GlobalPosition);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Explosion);
		SignalManager.EmitOnCreateObject(GlobalPosition, (int)GameObjectType.Pickup);
        SetPhysicsProcess(false);
		QueueFree();
    }


    
}
