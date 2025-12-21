using Godot;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	[Export] private float _moveTime = 0.1f;
	[Export] public NodePath LevelPath;
	private bool _moving = false;
	private Level _currLevel;

	public override void _Ready()
	{
		_currLevel = GetNode<Level>(LevelPath);
	}
	public override void _PhysicsProcess(double delta)
	{
		var dir = getInput();
		if (_moving || dir == Vector2.Zero || !_currLevel.canMoveTo(Position/Main.TileSize,dir))
		{
			return;
		}

		var targetPos = Position + Main.TileSize * dir;
		_moving = true;
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", targetPos, _moveTime);
		tween.Finished += () =>
		{
			_moving = false;
		};

	}

	private Vector2 getInput()
	{
		Vector2 dir = Vector2.Zero;
		if (Input.IsActionJustPressed("ui_up"))
		{
			dir = Vector2.Up;
		} else if (Input.IsActionJustPressed("ui_down"))
		{
			dir = Vector2.Down;
		} else if (Input.IsActionJustPressed("ui_right"))
		{
			dir = Vector2.Right;
		} else if (Input.IsActionJustPressed("ui_left"))
		{
			dir = Vector2.Left;
		}

		return dir;
	}

}
