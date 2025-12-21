using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using FragileSokoban.scripts;

public partial class Level : Node2D
{
	[Export] public NodePath TmlWallsPath;
	[Export] public NodePath TmlBackgroundPath;
	
	public CellType[,] grid;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fillGrid();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void fillGrid()
	{
		
		// Get size of the grid
		var bg= GetNode<TileMapLayer>(TmlBackgroundPath);
		var rect = bg.GetUsedRect();

		int width = rect.Size.X;
		int height = rect.Size.Y;
		
		grid = new CellType[width, height];
		
		
		// Fill it with empty slots
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				grid[i,j]=CellType.Empty;
			}
		}
		
		// Put the walls
		
		var walls = GetNode<TileMapLayer>(TmlWallsPath);
		foreach (Vector2 cell in walls.GetUsedCells())
		{
			grid[(int)cell.X-rect.Position.X,(int)cell.Y-rect.Position.Y] = CellType.Wall;
		}
		
		// Eventually add something to detect player and box
		
		
		//Print (for debug)

		for (int i = 0; i < width; i++)
		{
			var line = "";
			for (int j = 0; j < height; j++)
			{
				if (grid[i,j] == CellType.Empty)
				{
					line += ".";
				}
				else
				{
					line += "W";
				}
			}

			GD.Print(line);
		}
		
	}

	public bool canMoveTo(Vector2 target)
	{
		return grid[(int)target.X,(int)target.Y] == CellType.Empty;
	}
}
