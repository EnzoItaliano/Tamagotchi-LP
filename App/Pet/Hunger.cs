using Godot;
using System;

public class Hunger : Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public void update_hunger_text(string hunger) {
		Text = hunger;
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
