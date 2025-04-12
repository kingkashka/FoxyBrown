using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Variables
	private const float gravity = 690.0f;
	private const float jumpVelocity = -400.0f;
	private const float maxFall = 400.0f;
	private const float speed = 120.0f;
	#endregion

	#region Properties
	[Export] private Sprite2D characterSprite;
	[Export] private AudioStreamPlayer2D	jumpSound;

	#endregion
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = GetInput((float)delta);
		MoveAndSlide();
	}

    private Vector2 GetInput(float delta)
    {
        Vector2 newVelocity = Velocity;

		newVelocity.X = 0;
		newVelocity.Y += gravity * delta;

		if(Input.IsActionPressed("left"))
		{
			newVelocity.X -= speed;
			characterSprite.FlipH = true;
		} 
		else if(Input.IsActionPressed("right"))
		{
			newVelocity.X += speed;
			characterSprite.FlipH = false;
		}

		#region Jumping
		if(Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			newVelocity.Y = jumpVelocity;
			// SoundManager.PlayClip(jumpSound, SoundManager.SOUND_JUMP);
			jumpSound.Play();
		}
		newVelocity.Y = Mathf.Clamp(newVelocity.Y, jumpVelocity, maxFall);
		#endregion

		return newVelocity;
    }

}
