using Godot;
using System;

public partial class Player : CharacterBody2D
{
	#region Variables
	public const string GroupName = "Player";
	private const float gravity = 690.0f;
	private const float jumpVelocity = -400.0f;
	private const float maxFall = 400.0f;
	private const float speed = 120.0f;
	[Export] private float yFallOff = 100.0f;
	#endregion

	#region Properties
	[Export] private Label debugLabel;
	[Export] private Sprite2D characterSprite;
	[Export] private AudioStreamPlayer2D jumpSound;
	[Export] private AnimationPlayer animationPlayer;
	[Export] private Timer dashTimer;
	 

	#endregion

	private enum PlayerState
	{
		Idle,
		Running,
		Jumping,
		Falling,
		Hurting,
		Dashing
	}

	private PlayerState currentState = PlayerState.Idle;

	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Velocity = GetInput((float)delta);
		MoveAndSlide();
		DetermineState();
		UpdateDebugLabel();
		FallenOff();
	}

	private void UpdateDebugLabel()
	{
		string s = "";
		s += $"Velocity: {Velocity}\n";
		s += $"State: {currentState}\n";
		s += $"On Floor: {IsOnFloor()}";
		debugLabel.Text = s;
	}

	private void FallenOff()
	{
		if(GlobalPosition.Y > yFallOff)
		{
			SetPhysicsProcess(false);
		}
	}
	private Vector2 GetInput(float delta)
	{
		Vector2 newVelocity = Velocity;

		newVelocity.X = 0;
		newVelocity.Y += gravity * delta;

		if (Input.IsActionPressed("left"))
		{
			newVelocity.X -= speed;
			characterSprite.FlipH = true;

		}
		else if (Input.IsActionPressed("right"))
		{
			newVelocity.X += speed;
			characterSprite.FlipH = false;
		}

		#region Jumping
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			newVelocity.Y = jumpVelocity;
			// SoundManager.PlayClip(jumpSound, SoundManager.SOUND_JUMP);
			// jumpSound.Play();
		}
		newVelocity.Y = Mathf.Clamp(newVelocity.Y, jumpVelocity, maxFall);
		#endregion

		return newVelocity;
	}
	private void DetermineState()
	{
		PlayerState newState;

		if (IsOnFloor())
		{
			if (Velocity.X == 0)
			{
				newState = PlayerState.Idle;
			}
			else
			{
				newState = PlayerState.Running;
			}
		}
		else
		{
			if ((Velocity.Y < 0) && !IsOnFloor())
			{
				newState = PlayerState.Jumping;
			}
			else if (Velocity.Y > 0)
			{
				newState = PlayerState.Falling;
			}
			else
			{
				newState = PlayerState.Idle;
			}
		}
		// if(Input.IsActionJustPressed("Dash"))
		// {
		// 	float newVelocityX = Velocity.X;
		// 	newState = PlayerState.Dashing;
		// 	newVelocityX *= 2;
		// 	// gravity = 0;

		// 	// SoundManager.PlayClip(jumpSound, SoundManager.SOUND_JUMP)

		// }
		SetState(newState);
	}
	private void Dashing()
	{
		dashTimer.Timeout += () =>
		{
			SetState(PlayerState.Dashing);
			// Velocity.X *= 2;
			dashTimer.Stop();
		};
	}
	private void SetState(PlayerState newState)
	{
		if (newState == currentState)
			return;
		currentState = newState;

		switch (currentState)
		{
			case PlayerState.Idle:
				animationPlayer.Play("Idle");
				break;
			case PlayerState.Running:
				animationPlayer.Play("Run");
				break;
			case PlayerState.Jumping:
				animationPlayer.Play("Jump");
				break;
			case PlayerState.Falling:
				animationPlayer.Play("Fall");
				break;
			case PlayerState.Hurting:
				animationPlayer.Play("Hurt");
				break;
				case PlayerState.Dashing:
				animationPlayer.Play("Dash");
				break;
		}

	}

}
