using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectMaker : Node2D
{
	private Dictionary<GameObjectType, PackedScene> objectScenes = new();
	public override void _Ready()
	{
		objectScenes.Add(GameObjectType.BulletPlayer, GD.Load<PackedScene>("res://Scenes/PlayerBullet.tscn"));
		objectScenes.Add(GameObjectType.BulletEnemy, GD.Load<PackedScene>("res://Scenes/EnemyBullet.tscn"));
		objectScenes.Add(GameObjectType.Explosion, GD.Load<PackedScene>("res://Scenes/Explosion.tscn"));
		objectScenes.Add(GameObjectType.Pickup, GD.Load<PackedScene>("res://Scenes/FruitPickups/FruitPickup.tscn"));

		SignalManager.Instance.OnCreateBullet += OnCreateBullet;
		SignalManager.Instance.OnCreateObject += OnCreateExplosion;
		SignalManager.Instance.OnCreateObject += OnCreatePickup;
	}

	public override void _ExitTree()
	{
		SignalManager.Instance.OnCreateBullet -= OnCreateBullet;
		SignalManager.Instance.OnCreateObject -= OnCreateExplosion;
		SignalManager.Instance.OnCreateObject-= OnCreatePickup;
	}

	private void AddObject(Node node)
	{
		AddChild(node);
	}

	private void OnCreateBullet(Vector2 position, Vector2 direction, float speed, float lifeSpan, int gameObjectType)
	{
		GameObjectType goType = (GameObjectType)gameObjectType;
		BulletBase newScene = objectScenes[goType].Instantiate<BulletBase>();
		newScene.GlobalPosition = position;
		newScene.Setup(direction, lifeSpan, speed);
		// AddChild(newScene);
		CallDeferred(MethodName.AddObject, newScene);
	}
	private void OnCreateExplosion(Vector2 position, int gameObjectType)
	{
		GameObjectType goType = (GameObjectType)gameObjectType;
		Node2D	newScene = objectScenes[goType].Instantiate<Explosion>();
		newScene.GlobalPosition = position;
		CallDeferred(MethodName.AddObject, newScene);
	}

	private void OnCreatePickup(Vector2 position, int gameObjectType)
	{
		GameObjectType goType = (GameObjectType)gameObjectType;
		Node2D newScene = objectScenes[goType].Instantiate<FruitPickup>();
		newScene.GlobalPosition = position;
		CallDeferred(MethodName.AddObject, newScene);
	}
}
