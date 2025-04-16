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
		
		SignalManager.Instance.OnCreateBullet += OnCreateBullet;
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
		AddChild(newScene);
		CallDeferred(MethodName.AddObject, newScene);
    }

}
