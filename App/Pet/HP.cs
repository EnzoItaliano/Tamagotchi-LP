using Godot;
using System;

public class HP : Label {
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	public void update_health_text(string health) {
		Text = health;
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
