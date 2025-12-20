using Godot;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	[Export] public float Speed = 256f;

	public override void _PhysicsProcess(double delta)
	{
		GetPlayerInput();
		if (MoveAndSlide())
		{
			ResolveCollisions();
		}
	}

	private void GetPlayerInput()
	{
		Vector2 dir = Input.GetVector(
			"move_left",
			"move_right",
			"move_up",
			"move_down"
		);
		Velocity = dir * Speed;
	}

	private void ResolveCollisions()
	{
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			var body = (Box)collision.GetCollider();
			if (body != null)
			{
				Vector2 dir = Input.GetVector(
				"move_left",
				"move_right",
				"move_up",
				"move_down"
			);
				body.MoveBox(dir);
			}
		}
	}
}
