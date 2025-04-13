using Godot;
using System;

public partial class EnemyBase : CharacterBody2D
{
	[Export] private int points = 1;
	[Export] private float speed = 30.0f;
	[Export] private float yFallOff = 100.0f;
	protected float gravity = 800.0F;

	[Export] protected VisibleOnScreenNotifier2D visibleOnScreenNotifier2D;
	[Export] protected AnimatedSprite2D animatedSprite;

	[Export] protected Area2D hitBox;

	protected Player playerRef;
	public override void _Ready()
	{
		playerRef = GetTree().GetFirstNodeInGroup(Player.GroupName) as Player;
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
    private void OnScreenEntered()
    {
        throw new NotImplementedException();
    }


    private void OnScreenExited()
    {
        throw new NotImplementedException();
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
